using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class CreateBoardToTeamOperation
    {
        private const string BoardAddedToTeam = "Board {0} was added to team {1}!";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IWimFactory factory;

        public CreateBoardToTeamOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IWimFactory factory)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.factory = factory;
        }
        public string CreateBoardToTeam(string boardToAddToTeam, string teamForAddingBoardTo)
        {
            //Validations
            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToAddToTeam, boardTypeForChecking);

            var teamTypeForChecking = "Person Name";
            inputValidator.IsNullOrEmpty(teamForAddingBoardTo, teamTypeForChecking);

            inputValidator.ValdateBoardNameLength(boardToAddToTeam);

            inputValidator.ValidateTeamExistance(allTeams, teamForAddingBoardTo);

            inputValidator.ValidateBoardAlreadyInTeam(allTeams, boardToAddToTeam, teamForAddingBoardTo);

            //Operations
            var board = this.factory.CreateBoard(boardToAddToTeam);
            allTeams.AllTeamsList[teamForAddingBoardTo].AddBoard(board);

            return string.Format(BoardAddedToTeam, boardToAddToTeam, teamForAddingBoardTo);
        }

    }
}
