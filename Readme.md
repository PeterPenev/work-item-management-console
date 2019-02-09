Operations List in Application:

All Commands are Case sensitive!
In the bellow document you will find the mapping of commands in such order:

1. Principle Command Input
2. Real usable example given below

Enjoy your usage with the app!

* Create a new person
CreatePerson {personName}

Ex:
CreatePerson Vasil

* Show all people
ShowAllPeople

Ex:
ShowAllPeople

* Show person's activity
ShowPersonsActivity {personName}

Ex:
ShowPersonsActivity Vasil

* Create a new team
CreateTeam {teamName}

Ex:
CreateTeam alpha

* Show all teams
ShowAllTeams

Ex:
ShowAllTeams

* Show team's activity
ShowTeamsActivity {teamName}

Ex:
ShowTeamsActivity alpha

* Add person to team
AddPersonToTeam {personName} {teamName}

Ex:
AddPersonToTeam Vasil alpha

* Show all team members
ShowALlTeamMembers {teamName}

Ex:
ShowAllTeamMembers alpha

* Create a new board in a team
CreateBoard {boardName {teamName}

Ex:
CreateBoard boardTest alpha

* Show all team boards
ShowAllTeamBoards {teamName}

Ex:
ShowAllTeamBoards alpha

* Show board's activity
ShowBoardActivity {teamName} {boardName}

* Create a new Bug in a board
CreateBug {bugName} {teamName} {boardName} {priority} {severity} {assigneeName} !Steps #1. {stepName} #2. {stepName} #3. {stepName} !Steps {description in multiple words}

Ex:
CreateBug bugName01 alpha boardTest Low Minor Peter !Steps #1. open application #2. type command Create Bug #3. error message appear !Steps This is the description to the message

* Create a new Story in a board
CreateStory {storyName} alpha {boardName} {priority} {size} {status} {description}

Ex:
CreateStory storyName01 alpha boardTest High Medium InProgress Peter This is the first story in the Board

* Create a new Feedback in a board
CreateFeedback {feedbackName} alpha boardTest {rating} {status} {description}

Ex:
CreateFeedback feedbackName01 alpha boardTest 10 Done This is the first feedback in the Board

* Change Priority of a Bug
ChangeBugPriority {teamName} {boardName} {bugName} {newPriority} {assigneeName}

Ex:
ChangeBugPriority alpha boardTest bugName01 High Vasil

* Change Severity of a Bug
ChangeBugPriority {teamName} {boardName} {bugName} {newSeverity} {assigneeName}

Ex:
ChangeBugPriority alpha boardTest bugName01 Critical Vasil

* Change Status of a Bug
ChangeBugPriority {teamName} {boardName} {bugName} {newStatus} {assigneeName}

Ex:
ChangeBugPriority alpha boardTest bugName01 Fixed Vasil

* Change Priority of a story
ChangeStoryPriority {teamName} {boardName} {storyName} {newPriority} {assigneeName}

Ex:
ChangeStoryPriority alpha boardTest storyName01 Low Vasil

* Change Size of a story
ChangeStoryPriority {teamName} {boardName} {storyName} {newSize} {assigneeName}

Ex:
ChangeStoryPriority alpha boardTest storyName01 Small Vasil

* Change Status of a story
ChangeStoryPriority {teamName} {boardName} {storyName} {newStatus} {assigneeName}

Ex:
ChangeStoryPriority alpha boardTest storyName01 Done Vasil

* Change Rating of a feedback
ChangeFeedbackRating {teamName} {boardName} {feedbackName} {newRating} {assigneeName}

Ex:
ChangeFeedbackRating alpha boardTest feedbackName01 5 Vasil

* Change Status of a feedback
ChangeFeedbackRating {teamName} {boardName} {feedbackName} {newStatus} {assigneeName}

Ex:
ChangeFeedbackRating alpha boardTest feedbackName01 Done Vasil

* Assign/Unassign work item to a person
AssignUnassignBug {teamName} {boardName} {itemType} {itemName} {assigneeName}

Ex:
AssignUnassignBug alpha boardTest Bug bugName01 Vasil

* Add comment to a work item
AddComment {teamName} {boardName} {itemType} {itemName} {assigneeName} {comment description in multiple words}

Ex:
AddComment alpha boardTest Bug bugName01 Vasil "Tova e komentar"

* List all Work Items:
ListAllWorkItems

Ex:
ListAllWorkItems

* Filter bugs only
FilterBugs

Ex:
FilterBugs

* Filter stories only
FilterStories

Ex:
FilterStories

* Filter feedback only
FilterFeedbacks

Ex:
FilterFeedbacks

* FilterBugs by Assignee
FilterBugsByAssignee {assigneeName}

Ex:
FilterBugsByAssignee Vasil

* FilterBugs by Priority
FilterBugsByPriority {priority}

Ex:
FilterBugsByPriority High

* FilterBugs by Status
FilterBugsByStatus {status}

Ex:
FilterBugsByStatus Fixed

* FilterStories by Assignee
FilterStoriesByAssignee {assigneeName}

Ex:
FilterStoriesByAssignee Vasil

* FilterStories by Status
FilterStoriesByAssignee {status}

Ex:
FilterStoriesByAssignee Done

* FilterFeedbacks by Status
FilterFeedbacksByStatus {status}

Ex:
FilterFeedbacksByStatus Done


* SortBugsBy
SortBugsBy {typeToSortBy}

Ex:
SortBugsBy Title
SortBugsBy Priority

* SortStoriesBy
SortStories {typeToSortBy}

Ex:
SortStoriesBy Title
SortStoriesBy Priority

* SortFeedbacksBy
SortFeedbacksBy {typeToSortBy}

Ex:
SortFeedbacksBy Title
SortFeedbacksBy Rating


Demo 1:


Demo 2:


Demo 3: