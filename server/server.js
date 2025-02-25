// import our node_modules
import express from "express";
import cors from "cors";
import pg from "pg";
import dotenv from "dotenv";

// set up the app
const app = express();
app.use(cors());
app.use(express.json());
dotenv.config();

// set up our database connection
const db = new pg.Pool({ connectionString: process.env.DATABASE_URL });

// Create variables for the data to start with
var year_one, year_two, year_three, year_four;
// Create variables related to the raspberry Pi part of the API
var connected_users = [false, false, false, false, false, false];
var user_colours = [0, 0, 0, 0, 0, 0];

async function UpdateDatabaseCache() { // Runs indefinitely in the background, updates the cached database values hourly and on first execution
  FetchDatabase();
  while (true) {
    await new Promise(resolve => setTimeout(resolve, 600000));  
    FetchDatabase();
    console.log("Updated Database Cache!")
  } 
}
async function FetchDatabase() {
  year_one = await db.query("SELECT * FROM weather_2021",);
  year_two = await db.query("SELECT * FROM weather_2022",);
  year_three = await db.query("SELECT * FROM weather_2023",);
  year_four = await db.query("SELECT * FROM weather_2024",);
}
app.get("/", async function (request, response) { 
  var year = request.query["weather"];
  var connection = request.query["connection"];

  // Return weather data for each year
  switch(year) {
    case "2021": // Return 1/1/2021 -> 1/1/2022 weather data 
      response.json(year_one.rows);
      break;
    case "2022": // Return 1/1/2022 -> 1/1/2023 weather data 
      response.json(year_two.rows);
      break;
    case "2023": // Return 1/1/2023 -> 1/1/2024 weather data 
      response.json(year_three.rows);
      break;
    case "2024": // Return 1/1/2024 -> 1/1/2025 weather data 
      response.json(year_four.rows);
      break;
  } 

  // Data for the raspberry pi to connect and disconnect users
  switch(connection) {
    case "connect": // Provides a user id 
      var j = 0;
      connected_users.forEach(function (value, i) {
        if (!value && j == 0) {
          connected_users[i] = true;
          j = i
        }
      });
      response.json(j);
      break;
    case "getc": // Provides a user id
      response.json(user_colours);   
      break;
    default: // Disconnects the user with the id provided in the query
      connected_users[request.query["connection"]] = false;
      response.json(request.query["connection"]);
      break;
  }
});

// Set colour for a user's LEDs
app.get("/colour", async function (request, response) {
  if (Number(request.query["0"]) != NaN) {
    user_colours[0] = Number(request.query["0"]);
  }
  else if (Number(request.query["1"]) != NaN) {
    user_colours[1] = Number(request.query["1"]);
  }
  else if (Number(request.query["2"]) != NaN) {
    user_colours[2] = Number(request.query["2"]);
  }
  else if (Number(request.query["3"]) != NaN) {
    user_colours[3] = Number(request.query["3"]);
  }
  else if (Number(request.query["4"]) != NaN) {
    user_colours[4] = Number(request.query["4"]);
  }
  else if (Number(request.query["5"]) != NaN) {
    user_colours[5] = Number(request.query["5"]);
  }
  connected_users.forEach(function (value, i) {
    if (value == NaN) { // Ensure no NaN values are left over, this can happen if the query argument provided isn't a number
      connected_users[i] = 0; 
    }
  });
  console.log(user_colours);
  response.json("Set colour of user's LEDs");
});


// start the server
app.listen(8080, () => console.log("API Server is running on port 8080"));
UpdateDatabaseCache();

/*
Brief API Documentation;

Fetch weather data
https://hacked-zg4z.onrender.com/?weather=[year]
Replace square brackets with the year of the data you want to fetch, from 2021 to 2024.
Returns the weekly average throughout the specified year

Connect/Disconnect a client to the Pi;
https://hacked-zg4z.onrender.com/?connection=connect
Connect a new user to the Pi.
Returns the user ID
To disconnect a user, replace `connect` with the user ID of the user you want to disconnect
https://hacked-zg4z.onrender.com/?connection=[userid]

Set/Get the colour for the user's LEDs on the Pi Sense HAT;
https://hacked-zg4z.onrender.com/colour?[userid]=[colour]
Replace `userid` with the user's LEDs you want to change the colour of, and `colour` is the value/brightnes of the LED(s)
To get the colours;
https://hacked-zg4z.onrender.com/?connection=getc
This will return 6 values, with each user's colour.
*/