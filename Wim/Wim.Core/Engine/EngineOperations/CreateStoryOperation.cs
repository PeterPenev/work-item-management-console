using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Enums;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateStoryOperation : IEngineOperations
    {
        private const string StoryCreated = "Story {0} was created!";

        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IAllMembers allMembers;
        private readonly IWimFactory factory;
        private readonly IDescriptionBuilder descriptionBuilder;
        private readonly IMemberOpertaions memberOpertaions;
        private readonly IBoardOperations boardOperations;

        public CreateStoryOperation(
            IBusinessLogicValidator businessLogicValidator,
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IAllMembers allMembers,
            IWimFactory factory,
            IDescriptionBuilder descriptionBuilder,
            IMemberOpertaions memberOpertaions,
            IBoardOperations boardOperations)
        {
            this.businessLogicValidator = businessLogicValidator;
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.allMembers = allMembers;
            this.factory = factory;
            this.descriptionBuilder = descriptionBuilder;
            this.memberOpertaions = memberOpertaions;
            this.boardOperations = boardOperations;
        }

        public string Execute(IList<string> inputParameters)
        {
            string storyTitle = inputParameters[0];
            string teamToAddStoryFor = inputParameters[1];
            string boardToAddStoryFor = inputParameters[2];
            string storyPriority = inputParameters[3];
            string storySize = inputParameters[4];
            string storyStatus = inputParameters[5];
            string storyAssignee = inputParameters[6];
            var storyDescription = descriptionBuilder.BuildDescription(inputParameters, 7);

            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyTitle, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToAddStoryFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddStoryFor, boardTypeForChecking);

            inputValidator.ValdateItemTitleLength(storyTitle);

            inputValidator.ValdateItemDescriptionLength(storyDescription);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToAddStoryFor);

            businessLogicValidator.ValidateMemberExistance(allMembers, storyAssignee);

            businessLogicValidator.ValidateIfMemberNotInTeam(allTeams, teamToAddStoryFor, storyAssignee);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardToAddStoryFor, teamToAddStoryFor);

            businessLogicValidator.ValidateStoryExistanceInBoard(allTeams, boardToAddStoryFor, teamToAddStoryFor, storyTitle);

            //Operations

            var isPriorityEnumConvertable = Enum.TryParse(storyPriority, out Priority storyPriorityEnum);

            inputValidator.IsEnumConvertable(isPriorityEnumConvertable, "Priority");

            var isSizeEnumConvertable = Enum.TryParse(storySize, out Size storySizeEnum);

            inputValidator.IsEnumConvertable(isSizeEnumConvertable, "Size");

            var isStatusEnumConvertable = Enum.TryParse(storyStatus, out StoryStatus storyStatusEnum);

            inputValidator.IsEnumConvertable(isStatusEnumConvertable, "Status");

            IStory storyToAddToCollection = this.factory.CreateStory(storyTitle, storyDescription, storyPriorityEnum, storySizeEnum, storyStatusEnum, allMembers.AllMembersList[storyAssignee]);

            var indexOfBoardInSelectedTeam = allTeams.AllTeamsList[teamToAddStoryFor].Boards.FindIndex(boardIndex => boardIndex.Name == boardToAddStoryFor);

            boardOperations.AddWorkitemToBoard(allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam], storyToAddToCollection);

            var memberToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor].Members.First(member => member.Name == storyAssignee);

            memberOpertaions.AddWorkItemIdToMember(memberToPutHistoryFor, storyToAddToCollection.Id);

            var boardToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam];
            var teamToPutHistoryFor = allTeams.AllTeamsList[teamToAddStoryFor];

            boardOperations.AddActivityHistoryToBoard(allTeams.AllTeamsList[teamToAddStoryFor].Boards[indexOfBoardInSelectedTeam], memberToPutHistoryFor, storyToAddToCollection);

            memberOpertaions.AddActivityHistoryToMember(memberToPutHistoryFor, storyToAddToCollection, teamToPutHistoryFor, boardToPutHistoryFor);

            return string.Format(StoryCreated, storyTitle);
        }
    }
}
