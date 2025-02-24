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


app.get("/2021", async function (request, response) {
  const result = await db.query(
    "SELECT * FROM weather_2021",
  );
  response.json(result.rows);
});

app.get("/2022", async function (request, response) {
  const result = await db.query(
    "SELECT * FROM weather_2022",
  );
  response.json(result.rows);
});
app.get("/2023", async function (request, response) {
  const result = await db.query(
    "SELECT * FROM weather_2023",
  );
  response.json(result);
});
app.get("/2024", async function (request, response) {
  const result = await db.query(
    "SELECT * FROM weather_2024",
  );
  response.json(result.rows);
});

// start the server
app.listen(8080, () => console.log("App is running on PORT 8080"));