using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateBoardOperation : IEngineOperations
    {
        private const string BoardAddedToTeam = "Board {0} was added to team {1}!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IWimFactory factory;
        private readonly IBusinessLogicValidator businessLogicValidator;
        private readonly ITeamOperations teamOperations;

        public CreateBoardOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IWimFactory factory,
            IBusinessLogicValidator businessLogicValidator,
            ITeamOperations teamOperations)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.factory = factory;
            this.businessLogicValidator = businessLogicValidator;
            this.teamOperations = teamOperations;
        }
        public string Execute(IList<string> inputParameters)
        {
            string boardToAddToTeam = inputParameters[0];
            string teamForAddingBoardTo = inputParameters[1];

            //Validations
            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddToTeam, boardTypeForChecking);

            var teamTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(teamForAddingBoardTo, teamTypeForChecking);

            inputValidator.ValdateBoardNameLength(boardToAddToTeam);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamForAddingBoardTo);

            businessLogicValidator.ValidateBoardAlreadyInTeam(allTeams, boardToAddToTeam, teamForAddingBoardTo);

            //Operations
            var team = allTeams.AllTeamsList[teamForAddingBoardTo];
            var board = this.factory.CreateBoard(boardToAddToTeam);

            teamOperations.AddBoard(team, board);

            return string.Format(BoardAddedToTeam, boardToAddToTeam, teamForAddingBoardTo);
        }

    }
}
