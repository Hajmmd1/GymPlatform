using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.Modules.Membership.Domain.Enums;

namespace GymPlatform.Modules.Membership.Application.Commands.RegisterMember;

public sealed record RegisterMemberCommand(Guid GymId, string FullName, string Email, string? Phone, MemberStatus Status) : ICommand<MemberResponse>;