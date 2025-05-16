This is my final project for CIDM 3312. It is a piece of software designed for departments to manage inventory.

The first step is to make an account. This is done using ASP.NET Core's middleware library for Authentication and Authorization. When making an account, enter your name, password and choose a department. THe daprtment you choose is the items you will be allowed to modify. 

Once logged in, you can make your way to the inventory and view the role-based CRUD features. For example, if you chose the frozen department, you can modify the frozen items. 

You can also see which items will be going out of date soon, or are already out of date. Items that are expired appear red, and items nearing expiration are shown in yellow. 

The main point of this software is for both the stocker in charge of checking dates as well as management in ensuring it is getting done correctly. The button 'click here to confirm item is in date' creates a log. This log shows the user who performed the check, and at what date/time they checked it.

