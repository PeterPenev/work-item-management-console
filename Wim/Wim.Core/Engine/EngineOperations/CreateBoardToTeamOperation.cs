using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateBoardToTeamOperation : IEngineOperations
    {
        private const string BoardAddedToTeam = "Board {0} was added to team {1}!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IWimFactory factory;
        private readonly IBusinessLogicValidator businessLogicValidator;

        public CreateBoardToTeamOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IWimFactory factory,
            IBusinessLogicValidator businessLogicValidator)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.factory = factory;
            this.businessLogicValidator = businessLogicValidator;
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
            var board = this.factory.CreateBoard(boardToAddToTeam);
            allTeams.AllTeamsList[teamForAddingBoardTo].AddBoard(board);

            return string.Format(BoardAddedToTeam, boardToAddToTeam, teamForAddingBoardTo);
        }

    }
}
