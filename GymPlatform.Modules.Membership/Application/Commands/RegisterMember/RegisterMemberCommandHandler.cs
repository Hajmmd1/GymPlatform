using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.Modules.Membership.Domain.Entities;
using GymPlatform.Modules.Membership.Domain.Repositories;
using GymPlatform.Modules.Membership.Domain.ValueObjects;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Commands.RegisterMember;

public sealed class RegisterMemberCommandHandler : ICommandHandler<RegisterMemberCommand, MemberResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymRepository _gymRepository;
    private readonly IMemberRepository _memberRepository;

    public RegisterMemberCommandHandler(
        IUnitOfWork unitOfWork,
        IGymRepository gymRepository,
        IMemberRepository memberRepository)
    {
        _unitOfWork = unitOfWork;
        _gymRepository = gymRepository;
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberResponse>> HandleAsync(RegisterMemberCommand command, CancellationToken cancellationToken = default)
    {
        var gym = await _gymRepository.GetByIdAsync(command.GymId, cancellationToken);

        if (gym is null)
        {
            return Result<MemberResponse>.Failure("Gym not found.");
        }

        var existingMember = await _memberRepository.GetByEmailAndGymIdAsync(
            command.Email,
            command.GymId,
            cancellationToken);

        if (existingMember is not null)
        {
            return Result<MemberResponse>.Failure("A member with this email already exists in the gym.");
        }

        var email = new Email(command.Email);
        var phone = command.Phone is not null ? new Phone(command.Phone) : null;

        var member = new Member(
            command.GymId,
            command.FullName,
            email,
            gym.IsActive,
            phone,
            command.Status);

        await _memberRepository.AddAsync(member, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MemberResponse>.Success(new MemberResponse(
            member.Id,
            member.GymId,
            member.FullName,
            member.Email.Value,
            member.Phone?.Value,
            member.Status.ToString(),
            member.CreatedAt,
            member.AssignedCoachId));
    }
}