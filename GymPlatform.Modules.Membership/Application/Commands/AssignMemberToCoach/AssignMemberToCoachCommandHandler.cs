using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.Modules.Membership.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Commands.AssignMemberToCoach;

public sealed class AssignMemberToCoachCommandHandler : ICommandHandler<AssignMemberToCoachCommand, MemberCoachAssignmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymRepository _gymRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly ICoachRepository _coachRepository;

    public AssignMemberToCoachCommandHandler(
        IUnitOfWork unitOfWork,
        IGymRepository gymRepository,
        IMemberRepository memberRepository,
        ICoachRepository coachRepository)
    {
        _unitOfWork = unitOfWork;
        _gymRepository = gymRepository;
        _memberRepository = memberRepository;
        _coachRepository = coachRepository;
    }

    public async Task<Result<MemberCoachAssignmentResponse>> HandleAsync(AssignMemberToCoachCommand command, CancellationToken cancellationToken = default)
    {
        var member = await _memberRepository.GetByIdAsync(command.MemberId, cancellationToken);

        if (member is null)
        {
            return Result<MemberCoachAssignmentResponse>.Failure("Member not found.");
        }

        var gym = await _gymRepository.GetByIdAsync(member.GymId, cancellationToken);

        if (gym is null || !gym.IsActive)
        {
            return Result<MemberCoachAssignmentResponse>.Failure("Member cannot be assigned because the gym is inactive or not found.");
        }

        var coach = await _coachRepository.GetByIdAsync(command.CoachId, cancellationToken);

        if (coach is null)
        {
            return Result<MemberCoachAssignmentResponse>.Failure("Coach not found.");
        }

        if (coach.GymId != member.GymId)
        {
            return Result<MemberCoachAssignmentResponse>.Failure("Coach does not belong to the same gym as the member.");
        }

        member.AssignToCoach(command.CoachId);

        await _memberRepository.UpdateAsync(member, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MemberCoachAssignmentResponse>.Success(new MemberCoachAssignmentResponse(
            member.Id,
            command.CoachId,
            member.AssignedCoachId));
    }
}