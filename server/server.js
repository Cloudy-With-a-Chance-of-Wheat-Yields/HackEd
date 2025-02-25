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
    default:
      response.json("Successfully reached server!");
  } 
});

// start the server
app.listen(8080, () => console.log("API Server is running on port 8080"));
UpdateDatabaseCache();