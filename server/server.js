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

// create my test endpoint
app.get("/", (request, response) => response.json("Its working"));

// Request the data for each year from the database
const year_one = await db.query("SELECT * FROM weather_2021",);
const year_two = await db.query("SELECT * FROM weather_2022",);
const year_three = await db.query("SELECT * FROM weather_2023",);
const year_four = await db.query("SELECT * FROM weather_2024",);

app.get("/2021", async function (request, response) { // Return 1/1/2021 -> 1/1/2022 weather data 
  response.json(year_one.rows);
});

app.get("/2022", async function (request, response) { // Return 1/1/2022 -> 1/1/2023 weather data 
  response.json(year_two.rows);
});
app.get("/2023", async function (request, response) { // Return 1/1/2023 -> 1/1/2024 weather data 
  response.json(year_three.rows);
});
app.get("/2024", async function (request, response) { // Return 1/1/2024 -> 1/1/2025 weather data 
  response.json(year_four.rows);
});

// start the server
app.listen(8080, () => console.log("API Server is running on port 8080"));