# BlazorServerSideWithSharedStartup

Demonstration showing that blazor ignores components in the `Shared/` folder when the `Startup` type is located in a different project.

* Running `TodoList/Program.cs` using the `WorkingStartup` (which is located in the `TodoList` project) causes the Navigation menu and layout to be rendered correctly.
* Running `TodoList/Program.cs` using the `BrokenStartup` (which is located in the `Plumbing` project) causes the Navigation menu and layout to not be rendered at all.
