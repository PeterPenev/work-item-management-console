====WimApp ver. 2.0 Highlights====
Team Name: Team 7
Developers: 
	·Dimitar Mihov - Gitlab @dimitarmihov
	·Petar Penev - GitLab @peterpenev; 
	·Vasil Prodanov - GitLab @vasilprodanov

Problems from version 1.0:
1) Application had both business and input validtions in single class.
2) Application had multiple-responsibilities entrusted onto the Engine.
3) Application had tightly coupled relationships between classes.
4) Application had Models containing methods inside them.
5) Application did not include any Unit Tests.
6) Application used redundant EnumParser class.	
	
Changes Made to version 2.0 since version 1.0:
1) Seperate Engine Methods into classes with Single Responsibility;
2) Use AutoFac to resolve Dependencies and Find Commands of IEngineOperations interface;
3) Inject Dependencies of every class via constructor Injection;
4) Seperate InputValidator into InputValidator and BusinessValidator;
5) Refactor EnumParser into Enum.TryParse method;
6) Seperate Model Classes' methods from the Model itself into Model Operations working on the concrete Model;
7) Write Unit tests;
8) Created ClassDiagram for Wim.Models

Design Patterns used:
1) Factory Design Pattern
2) Dependency Inversion Pattern

====Operations List in Application====

All Commands are Case sensitive!
In the bellow document you will find the mapping of commands in such order:

1. Principle Command Input
2. Real usable example given below

Enjoy your usage with the app!

***** Create a new person *****
CreatePerson {personName}

Ex:
CreatePerson Vasil

***** Show all people *****
ShowAllPeople

Ex:
ShowAllPeople

***** Show person's activity *****
ShowPersonsActivity {personName}

Ex:
ShowPersonsActivity Vasil

***** Create a new team *****
CreateTeam {teamName}

Ex:
CreateTeam alpha

***** Show all teams *****
ShowAllTeams

Ex:
ShowAllTeams

***** Show team's activity *****
ShowTeamsActivity {teamName}

Ex:
ShowTeamsActivity alpha

***** Add person to team *****
AddPersonToTeam {personName} {teamName}

Ex:
AddPersonToTeam Vasil alpha

***** Show all team members *****
ShowALlTeamMembers {teamName}

Ex:
ShowAllTeamMembers alpha

***** Create a new board in a team *****
CreateBoard {boardName {teamName}

Ex:
CreateBoard boardTest alpha

***** Show all team boards *****
ShowAllTeamBoards {teamName}

Ex:
ShowAllTeamBoards alpha

***** Show board's activity *****
ShowBoardActivity {teamName} {boardName}

***** Create a new Bug in a board *****
CreateBug {bugName} {teamName} {boardName} {priority} {severity} {assigneeName} !Steps #1. {stepName} #2. {stepName} #3. {stepName} !Steps {description in multiple words}

Ex:
CreateBug bugName01 alpha boardTest Low Minor Peter !Steps #1. open application #2. type command Create Bug #3. error message appear !Steps This is the description to the message

***** Create a new Story in a board *****
CreateStory {storyName} alpha {boardName} {priority} {size} {status} {description}

Ex:
CreateStory storyName01 alpha boardTest High Medium InProgress Peter This is the first story in the Board

***** Create a new Feedback in a board *****
CreateFeedback {feedbackName} alpha boardTest {rating} {status} {description}

Ex:
CreateFeedback feedbackName01 alpha boardTest 10 Done This is the first feedback in the Board

***** Change Priority of a Bug *****
ChangeBugPriority {teamName} {boardName} {bugName} {newPriority} {assigneeName}

Ex:
ChangeBugPriority alpha boardTest bugName01 High Vasil

***** Change Severity of a Bug *****
ChangeBugPriority {teamName} {boardName} {bugName} {newSeverity} {assigneeName}

Ex:
ChangeBugPriority alpha boardTest bugName01 Critical Vasil

***** Change Status of a Bug *****
ChangeBugPriority {teamName} {boardName} {bugName} {newStatus} {assigneeName}

Ex:
ChangeBugPriority alpha boardTest bugName01 Fixed Vasil

***** Change Priority of a story *****
ChangeStoryPriority {teamName} {boardName} {storyName} {newPriority} {assigneeName}

Ex:
ChangeStoryPriority alpha boardTest storyName01 Low Vasil

***** Change Size of a story *****
ChangeStoryPriority {teamName} {boardName} {storyName} {newSize} {assigneeName}

Ex:
ChangeStoryPriority alpha boardTest storyName01 Small Vasil

***** Change Status of a story *****
ChangeStoryPriority {teamName} {boardName} {storyName} {newStatus} {assigneeName}

Ex:
ChangeStoryPriority alpha boardTest storyName01 Done Vasil

***** Change Rating of a feedback *****
ChangeFeedbackRating {teamName} {boardName} {feedbackName} {newRating} {assigneeName}

Ex:
ChangeFeedbackRating alpha boardTest feedbackName01 5 Vasil

***** Change Status of a feedback *****
ChangeFeedbackRating {teamName} {boardName} {feedbackName} {newStatus} {assigneeName}

Ex:
ChangeFeedbackRating alpha boardTest feedbackName01 Done Vasil

***** Assign/Unassign work item to a person *****
AssignUnassignBug {teamName} {boardName} {itemType} {itemName} {assigneeName}

Ex:
AssignUnassignBug alpha boardTest Bug bugName01 Vasil

***** Add comment to a work item *****
AddComment {teamName} {boardName} {itemType} {itemName} {assigneeName} {comment description in multiple words}

Ex:
AddComment alpha boardTest Bug bugName01 Vasil "Tova e komentar"

***** List all Work Items *****
ListAllWorkItems

Ex:
ListAllWorkItems

***** Filter bugs only *****
FilterBugs

Ex:
FilterBugs

***** Filter stories only *****
FilterStories

Ex:
FilterStories

***** Filter feedback only *****
FilterFeedbacks

Ex:
FilterFeedbacks

***** FilterBugs by Assignee *****
FilterBugsByAssignee {assigneeName}

Ex:
FilterBugsByAssignee Vasil

***** FilterBugs by Priority *****
FilterBugsByPriority {priority}

Ex:
FilterBugsByPriority High

***** FilterBugs by Status *****
FilterBugsByStatus {status}

Ex:
FilterBugsByStatus Fixed

***** FilterStories by Assignee *****
FilterStoriesByAssignee {assigneeName}

Ex:
FilterStoriesByAssignee Vasil

***** FilterStories by Status *****
FilterStoriesByAssignee {status}

Ex:
FilterStoriesByAssignee Done

***** FilterFeedbacks by Status *****
FilterFeedbacksByStatus {status}

Ex:
FilterFeedbacksByStatus Done


***** SortBugsBy *****
SortBugsBy {typeToSortBy}

Ex:
SortBugsBy Title
SortBugsBy Priority

***** SortStoriesBy *****
SortStories {typeToSortBy}

Ex:
SortStoriesBy Title
SortStoriesBy Priority

***** SortFeedbacksBy *****
SortFeedbacksBy {typeToSortBy}

Ex:
SortFeedbacksBy Title
SortFeedbacksBy Rating

=============
Demo 1 -- All Commands are Valid
=============

CreatePerson Peter
CreatePerson Vasil
CreatePerson Mitko
CreateTeam Alpha
CreateTeam Beta
CreateBoard board01 Alpha
CreateBoard board02 Alpha
AddPersonToTeam Peter Alpha
AddPersonToTeam Vasil Alpha
AddPersonToTeam Mitko Alpha
CreateBug bug12345678 Alpha board01 Low Minor Peter !Steps #1. open application #2. type command Create Bug #3. error message appear !Steps First description to the message
CreateBug bug23456789 Alpha board01 High Major Vasil !Steps #1. open application #2. type command Create Bug #3. error message appear !Steps Second description to the message
CreateStory story123456 Alpha board01 High Medium InProgress Peter This is the first story in the Board
CreateStory story654321 Alpha board01 Medium Large NotDone Vasil This is the second story in the Board
CreateStory storystory123 Alpha board01 Low Small Done Mitko This is the third story in the Board
CreateFeedback feedback123456 Alpha board01 10 Done This is the first feedback in the Board
CreateFeedback feedback654321 Alpha board01 9 New This is the second feedback in the Board
ChangeBugPriority Alpha board01 bug12345678 High Peter
ChangeBugSeverity Alpha board01 bug12345678 Critical Peter
ChangeBugStatus Alpha board01 bug12345678 Fixed Peter
ChangeStoryPriority Alpha board01 story123456 Low Peter
ChangeStorySize Alpha board01 story123456 Small Peter
ChangeStoryStatus Alpha board01 story123456 Done Peter
ChangeFeedbackRating Alpha board01 feedback123456 5 Peter
ChangeFeedbackStatus Alpha board01 feedback123456 Done Peter
AddComment Alpha board01 Bug bug12345678 Peter "Tova e komentar"
ListAllWorkItems
FilterBugs
FilterStories
FilterFeedbacks
FilterBugsByPriority Low
FilterBugsByAssignee Peter
FilterBugsByStatus Active
FilterStoriesByPriority High
FilterStoriesByAssignee Mitko
FilterStoriesByStatus Done
FilterFeedbacksByStatus Done
SortBugsBy title
SortBugsBy priority
SortBugsBy severity
SortStoriesBy title
SortStoriesBy priority
SortStoriesBy size
SortFeedbacksBy rating
AssignUnassignItem Alpha board01 Bug bug23456789 Peter
ShowPersonsActivity Peter
ShowBoardActivity Alpha board01
ShowTeamsActivity Alpha


======================
Demo 2 -- WARNING -- SOME LINES CONTAIN FALSE OPERATIONS TO SHOW MISTAKE-HANDLING! 
Wrong Commands #2; #4; #7;
======================

CreateTeam Beta
CreatePerson Ivan
CreatePerson Vasil
AddPersonToTeam Ivan Beta
AddPersonToTeam Vasil Beta
CreateBoard testBoard Beta
CreateBug bug12345678 Alpha testBoard Low Minor Vasil !Steps #1. open application #2. type command Create Bug #3. error message appear !Steps First description to the message

======================
Demo 3 -- Short Demo - All Commands are Valid!
======================

CreateTeam Alpha
CreatePerson Peter
CreatePerson Mitko
AddPersonToTeam Peter Alpha
AddPersonToTeam Mitko Alpha
CreateBoard workBoard Alpha
CreateStory story123456 Alpha workBoard High Medium InProgress Peter This is the first story in the Board
CreateFeedback feedback123456 Alpha workBoard 10 Done This is the first feedback in the Board
ListAllWorkItems
FilterStories 

======================
END of Example Demos
======================

____________________________Type Your Commands Below____________________________

