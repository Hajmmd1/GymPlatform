using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPlatform.Infrastructure.Persistence.Migrations
{
    public partial class TrainingInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exercises",
                schema: "training",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    difficulty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    coach_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    required_equipment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workout_programs",
                schema: "training",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    difficulty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    coach_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    duration_weeks = table.Column<int>(type: "int", nullable: false),
                    exercise_count = table.Column<int>(type: "int", nullable: false),
                    version = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    is_published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workout_programs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workout_logs",
                schema: "training",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    program_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sets_completed = table.Column<int>(type: "int", nullable: true),
                    reps_completed = table.Column<int>(type: "int", nullable: true),
                    weight_used = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    logged_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    completed_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_completed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workout_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exercise_videos",
                schema: "training",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    video_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    thumbnail_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    duration_seconds = table.Column<int>(type: "int", nullable: true),
                    coach_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    view_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercise_videos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "body_measurements",
                schema: "training",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    measured_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    recorded_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_body_measurements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "progress_photos",
                schema: "training",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    photo_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    captured_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    uploaded_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    thumbnail_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_private = table.Column<bool>(type: "bit", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_progress_photos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coach_profiles",
                schema: "training",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    coach_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bio = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    profile_photo_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    rating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    rating_count = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coach_profiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workout_program_exercises",
                schema: "training",
                columns: table => new
                {
                    program_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order = table.Column<int>(type: "int", nullable: false),
                    exercise_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sets = table.Column<int>(type: "int", nullable: false),
                    reps = table.Column<int>(type: "int", nullable: false),
                    rest_seconds = table.Column<int>(type: "int", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workout_program_exercises", x => new { x.program_id, x.order });
                    table.ForeignKey(
                        name: "fk_workout_program_exercises_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalSchema: "training",
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercise_primary_muscle_groups",
                schema: "training",
                columns: table => new
                {
                    exercise_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    muscle_group = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercise_primary_muscle_groups", x => new { x.exercise_id, x.muscle_group });
                    table.ForeignKey(
                        name: "fk_exercise_primary_muscle_groups_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalSchema: "training",
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercise_secondary_muscle_groups",
                schema: "training",
                columns: table => new
                {
                    exercise_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    muscle_group = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exercise_secondary_muscle_groups", x => new { x.exercise_id, x.muscle_group });
                    table.ForeignKey(
                        name: "fk_exercise_secondary_muscle_groups_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalSchema: "training",
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "coach_certifications",
                schema: "training",
                columns: table => new
                {
                    coach_profile_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    issuing_organization = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    expires_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    obtained_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coach_certifications", x => new { x.coach_profile_id, x.name });
                    table.ForeignKey(
                        name: "fk_coach_certifications_coach_profiles_coach_profile_id",
                        column: x => x.coach_profile_id,
                        principalSchema: "training",
                        principalTable: "coach_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_exercises_coach_id",
                schema: "training",
                table: "exercises",
                column: "coach_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_programs_coach_id",
                schema: "training",
                table: "workout_programs",
                column: "coach_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_program_exercises_exercise_id",
                schema: "training",
                table: "workout_program_exercises",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_videos_exercise_id",
                schema: "training",
                table: "exercise_videos",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_videos_coach_id",
                schema: "training",
                table: "exercise_videos",
                column: "coach_id");

            migrationBuilder.CreateIndex(
                name: "ix_body_measurements_member_id",
                schema: "training",
                table: "body_measurements",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "ix_progress_photos_member_id",
                schema: "training",
                table: "progress_photos",
                column: "member_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coach_certifications",
                schema: "training");

            migrationBuilder.DropTable(
                name: "exercise_secondary_muscle_groups",
                schema: "training");

            migrationBuilder.DropTable(
                name: "exercise_primary_muscle_groups",
                schema: "training");

            migrationBuilder.DropTable(
                name: "workout_program_exercises",
                schema: "training");

            migrationBuilder.DropTable(
                name: "body_measurements",
                schema: "training");

            migrationBuilder.DropTable(
                name: "progress_photos",
                schema: "training");

            migrationBuilder.DropTable(
                name: "exercise_videos",
                schema: "training");

            migrationBuilder.DropTable(
                name: "workout_logs",
                schema: "training");

            migrationBuilder.DropTable(
                name: "workout_programs",
                schema: "training");

            migrationBuilder.DropTable(
                name: "exercises",
                schema: "training");
        }
    }
}