# Aeroplay WebApp
### High-Level Overview
The finished product is available at [aeroplay.online](https://aeroplay.online)

If the link above does not work, try [this one](https://aeroplay.netlify.app/) (We only had the domain for one year)

The web application is a full stack CRUD application that makes use of React, Express, and MySQL to create the frontend and backend applications. We chose to use these methodologies and modules to make the application more responsive as opposed to having a simple flat page or constant redirects. React is used to construct the frontend client pages while Express and MySQL are used to create the backend server. The client and the server are able to communicate with each other using RESTful API. Additonally, NodeJS is used in both the frontend and backend to create a runtime environment outside the browser for development purposes. For deployment, we are using Heroku, ClearDB, and Nelify. The backend also has implementations for requests that are accessed by our game build which allows us to store user data in order to do authentication, preference saving, and achievements.
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

The MySQL database contains a relation called `game_data` which contains all the game entries. Each entry contains a unique `game_id`, as well as the associated `username`, `score`, `balloons_popped`, `shot_accuracy`, `boosts_used`, `game_map`, and `control` fields. The schema is as follows:
```
`game_id` int(11) NOT NULL AUTO_INCREMENT,
`username` varchar(32) NOT NULL,
`score` int(11) NOT NULL,
`balloons_popped` int(11) NOT NULL,
`shot_accuracy` float NOT NULL,
`boosts_used` int(11) NOT NULL,
`game_map` varchar(9) NOT NULL,
`control` varchar(8) NOT NULL,
PRIMARY KEY (`game_id`)
```
the `game_id` must be unique for each game and is used as a primary key; we can make sure that each ID is unique using the AUTO_INCREMENT built-in. We also need to make sure that none of the input values are NULL because they are considered invalid.

The database also stores a relation called `user_data` which contains information for each user. A user is uniquely identified using the `username` field, with each user entry having a field for `hash`, `salt`, `music_volume`, `engine_volume`, `minimap`, `retro_camera`, and `daytime`. A user needs both a username and a password to authenticate and to create a new entry, we first generate a random salt to append to the password then use the SHA256 one-way encryption algorithm to encrypt and store the password. We can perform the same process to check whether a given input is the valid password or not. The other fields in this relation correspond to the settings that the user has saved from a previous session. The schema is as follows:
```
`username` varchar(32) NOT NULL,
`hash` varchar(64) DEFAULT NULL,
`salt` varchar(32) DEFAULT NULL,
`music_volume` int(11) NOT NULL DEFAULT '100',
`engine_volume` int(11) NOT NULL DEFAULT '100',
`minimap` int(11) NOT NULL DEFAULT '1',
`retro_camera` int(11) NOT NULL DEFAULT '0',
`daytime` varchar(6) NOT NULL DEFAULT 'sunset',
PRIMARY KEY (`username`)
```

Additionally, we have a relation called `achievements` that stores the id number and name for each achievement. This is used to associate the id number of an achievement with the name and badge icon that should appear on the website. The schema is as follows:
```
`id` int(11) NOT NULL,
`name` varchar(100) NOT NULL,
`description` varchar(100) NOT NULL,
`list_order` int(11) NOT NULL,
PRIMARY KEY (`id`)
```

Finally, we have a relation called `achievement_data` that stores the achievements that a given player has earned. Each unique `username` has 39 associated attributes, `id0` through `id38`. Each of these ids store a value that indicates the progress on a given achievement. Most achievements are either 0 or 1 for no or yes, but some achievements like the "games played" achievements store the progress of an achievement using a counter. The schema is as follows:
```
`username` varchar(32) NOT NULL,
`id0` int(11) DEFAULT '1',
`id1` int(11) DEFAULT '0',
`id2` int(11) DEFAULT '0',
`id3` int(11) DEFAULT '0',
`id4` int(11) DEFAULT '0',
`id5` int(11) DEFAULT '0',
`id6` int(11) DEFAULT '0',
`id7` int(11) DEFAULT '0',
`id8` int(11) DEFAULT '0',
`id9` int(11) DEFAULT '0',
`id10` int(11) DEFAULT '0',
`id11` int(11) DEFAULT '0',
`id12` int(11) DEFAULT '0',
`id13` int(11) DEFAULT '0',
`id14` int(11) DEFAULT '0',
`id15` int(11) DEFAULT '0',
`id16` int(11) DEFAULT '0',
`id17` int(11) DEFAULT '0',
`id18` int(11) DEFAULT '0',
`id19` int(11) DEFAULT '0',
`id20` int(11) DEFAULT '0',
`id21` int(11) DEFAULT '0',
`id22` int(11) DEFAULT '0',
`id23` int(11) DEFAULT '0',
`id24` int(11) DEFAULT '0',
`id25` int(11) DEFAULT '0',
`id26` int(11) DEFAULT '0',
`id27` int(11) DEFAULT '0',
`id28` int(11) DEFAULT '0',
`id29` int(11) DEFAULT '0',
`id30` int(11) DEFAULT '0',
`id31` int(11) DEFAULT '0',
`id32` int(11) DEFAULT '0',
`id33` int(11) DEFAULT '0',
`id34` int(11) DEFAULT '0',
`id35` int(11) DEFAULT '0',
`id36` int(11) DEFAULT '0',
`id37` int(11) DEFAULT '0',
`id38` int(11) DEFAULT '0',
PRIMARY KEY (`username`)
```

`index.js` is a simple file that makes a connection to the database and then listens for requests, routing them to the appropriate handler. Here are the valid requests and their functions:

| Request | Type | Function |
| ------ | ------  | ------ |
| /api/get | GET | returns a sorted list of all game data entries |
| /api/user | POST | returns a sorted list of all game data entries for a given user |
| /api/insert | POST | inserts a game data entry into the database with the given inputs |
| /api/player/data | POST | returns the authentication and settings data for a given user |
| /api/player/insert | POST | inserts a new entry for a new user with provided authentication details |
| /api/player/settings | POST | updates the player settings entry for a given user |
| /api/achievements | GET | returns a list of all achievement ids and the achievement name |
| /api/achievements/new | POST | inserts a new achievement history for a new given user |
| /api/achievements/get | POST | returns the achievement history for a given user |
| /api/achievements/update | POST | updates the status of a single achievment for a single given user |

### Frontend
The frontend for this web application is hosted by Netlify with a custom domain name and is built using React. This will allow us to display a page, respond to user input, send requests to the backend, and return and process results to display to the user.

There are currently two different pages for the web application. Global mode shows the games and statistics for the entire player base, and player mode which only shows the games and achievements for a given player. We chose to include two modes because we want players to be able to compete with not only with other players' scores, but their own scores as well. Additonally, the game has multiple maps with different playstyles so we have a separate leaderboard for each map and controller setup.

Users are also able to download the game from the website via a button located on the main page as well as a link to the user manual. We use the website as a means to distribute the game because downloading is intuitive for many internet users.

In `App.js`, we will be using the `useState` and `useEffect` React hooks to get the user's inputs and respond to user actions. `useState` can be used to store information like the user's search query or the current list of game entries, and `useEffect` can be used to detect changes in the page from user inputs. When a user presses a button, the `useEffect` hook is triggered, sending a request (with user input if relevant) to the backend application. The reason why we have the user press enter to send input instead of it automatically sending input on change is because the free database option we are using has a limited number of questions and querying on every update would quickly fill the quota. We will get a response from the backend application in the form of a JSON string which we can parse. We can simply use a loop to parse the JSON objects to display the information or in the case of the global/player statistics, we can create a function `getGlobalStats` that processes the data further.

`App.js` also handles the HTML for the web application, which consists of a few headers, a search input text box, a couple buttons, and multiple tables for displaying data. The tables are automatically updated to reflect the information that is returned from the backend application. The css is handled by `App.css` which creates represents the style sheet of the client. Here, we define attributes for the DOM objects that makes the frontend application prettier and more user friendly.

In `Player.js`, we use a similar setup to `App.js` but adapted to only show scores for the username passed in through the url parameter. Additionally, we will pull the achievements from the backend server as well as the achievement progress for the given username. Using the id number of an achievement and the value obtained from the achievement progress JSON, we can find the appropriate achievment badge icon to display.

There is absolutely nothing special about clicking the Aeroplay logo at the top.

### Future Improvements:
We can improve the web application by adding in an instructions page instead of redirecting to a user manual. We can also add the ability to change the in game settings from the website.

### Note:
The frontend and backend need to be hosted from their own standalone Git repositories so the files here are simply copies of the core application code. There is a lot more structure and more code involved that is required to deploy and run the services, but those files are unchanged. We have chosen not to copy all of those files here because items like node modules are notoriously bulky and would make `git clone` substantially more time consuming while adding nothing of value.

Frontend repo: [https://github.com/Ericzklm/Aeroplay-Web_Client](https://github.com/Ericzklm/Aeroplay-Web_Client)
Backend repo: [https://git.heroku.com/aeroplay.git](https://git.heroku.com/aeroplay.git) (permissions needed to clone)