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

// POST endpoint
app.post("/messages", async function (request, response) {
  // get the request body (data from the form)
  const { message, name } = request.body; // destructuring data

  // make the query to the db
  const result = await db.query(
    "INSERT INTO messages (name, message) VALUES ($1, $2)",
    [name, message]
  );

  // send back the data from the db as a response
  response.json(result);
});

// start the server
app.listen(8080, () => console.log("App is running on PORT 8080"));