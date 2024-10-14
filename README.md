# Debugging Interview Code
## Steps
1. Clone this repository.
2. Open the project in your favorite IDE.
3. Run the project.
4. Go to swagger, eg. `http://localhost:5041/swagger`.
5. Call the `GET /api/touchpoints` endpoint.
    - **Should return 404 when there are no touchpoints.**
6. *Fix the issue and try again.*
7. Call the `POST /api/touchpoints` endpoint with the following payload:
    ```json
    {
      "description": "Touchpoint 1"
    }
    ```
    - **Should return 400 Bad Request with relevant message.**
8. Call the `POST /api/touchpoints` endpoint again with the following payload:
    ```json
    {
      "name": "Touchpoint 1"
    }
    ```
    - **Should have a similar response as the last call.**
9. *Fix the issue and try again.*
10. Call the `POST /api/touchpoints` endpoint again with the following payload:
    ```json
    {
      "name": "Touchpoint 1",
      "description": "Touchpoint 1"
    }
    ```
    - **Returns 200 but the touchpoint isn't being created as expected.**
11. *Fix the issue and try again.*
