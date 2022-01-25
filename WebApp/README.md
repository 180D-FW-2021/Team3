# Aeroplay WebApp
### High-Level Overview
The finished product is available at [aeroplay.online](https://aeroplay.online)

The web application is a full stack CRUD application that makes use of React, Express, and MySQL to create the frontend and backend applications. We chose to use these methodologies and modules to make the application more responsive as opposed to having a simple flat page or constant redirects. React is used to construct the frontend client page while Express and MySQL are used to create the backend server. The client and the server are able to communicate with each other using RESTful API. Additonally, NodeJS is used in both the frontend and backend to create a runtime environment outside the browser for development purposes. For deployment, we are using Heroku, ClearDB, and Nelify.
| Module | Link |
| ------ | ------ |
| React | https://reactjs.org/ |
| Express | https://expressjs.com/ |
| MySQL | https://www.mysql.com/ |
| Node | https://nodejs.org/en/ |
| Heroku | https://www.heroku.com/ |
| ClearDB | https://www.cleardb.com/ |
| Netlify | https://www.netlify.com/ |
We chose these modules specifically because they are the most common and are standard in the industry (they are also all free to use!).

### Backend
The backend for this web application is hosted by Heroku with a ClearDB MySQL add-on and is built using Express. This allows us to send requests to the web server which will fetch and return the necessary data from our database in the form of a JSON string.

The MySQL database contains a single relation called `game_data` which contains all the game entries. Each entry contains a unique `game_id`, as well as the associated `username`, `score`, `balloons_popped`, `shot_accuracy`, and `boosts_used` fields. The schema is as follows:
```
`game_id` int(11) NOT NULL AUTO_INCREMENT,
`username` varchar(32) NOT NULL,
`score` int(11) NOT NULL,
`balloons_popped` int(11) NOT NULL,
`shot_accuracy` float NOT NULL,
`boosts_used` int(11) NOT NULL,
PRIMARY KEY (`game_id`)
```
the `game_id` must be unique for each game and is used as a primary key; we can make sure that each ID is unique using the AUTO_INCREMENT built-in. We also need to make sure that none of the input values are NULL because they are considered invalid.

`index.js` is a simple file that makes a connection to the database and then listens for requests, routing them to the appropriate handler. Here are the valid requests and their functions:

| Request | Type | Function |
| ------ | ------  | ------ |
| /api/get | GET | returns a sorted list of all game data entries |
| /api/user | POST | returns a sorted list of all game data entries for a given user |
| /api/insert | POST | inserts a game data entry into the database with the given inputs |

### Frontend
The frontend for this web application is hosted by Netlify with a custom domain name and is built using React. This will allow us to display a page, respond to user input, send requests to the backend, and return and process results to display to the user.

There are currently two different modes for the web application. Global mode shows the games and statistics for the entire player base, and player mode which only shows the games and statistics for a given player. We chose to include two modes because we want players to be able to compete with not only with other players' scores, but their own scores as well.

In `App.js`, we will be using the `useState` and `useEffect` React hooks to get the user's inputs and respond to user actions. `useState` can be used to store information like the user's search query or the current list of game entries, and `useEffect` can be used to detect changes in the page from user inputs. When a user presses a button, the `useEffect` hook is triggered, sending a request (with user input if relevant) to the backend application. The reason why we have the user press a button to send input instead of it automatically sending input on change is because the free database option we are using has a limited number of questions and querying on every update would quickly fill the quota. We will get a response from the backend application in the form of a JSON string which we can parse. We can simply use a loop to parse the JSON objects to display the information or in the case of the global/player statistics, we can create a function `getGlobalStats` that processes the data further.

`App.js` also handles the HTML for the web application, which consists of a few headers, a search input text box, a couple buttons, and multiple tables for displaying data. The tables are automatically updated to reflect the information that is returned from the backend application. The css is handled by `App.css` which creates represents the style sheet of the client. Here, we define attributes for the DOM objects that makes the frontend application prettier and more user friendly.

### Future Improvements:
With the introduction of multiple maps, we want to be able to keep track of the map that a player used and separate scores based on map. Additionally, we can use the web application to save a user's options loadout in game.

### Note:
The frontend and backend need to be hosted from their own standalone Git repositories so the files here are simply copies of the core application code. There is a lot more structure and more code involved that is required to deploy and run the services, but those files are unchanged. We have chosen not to copy all of those files here because items like node modules are notoriously bulky and would make `git clone` substantially more time consuming while adding nothing of value.

[frontend repo:](https://github.com/Ericzklm/Aeroplay-Web_Client)