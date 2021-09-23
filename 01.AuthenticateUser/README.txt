To autehnticate user using personal access token 
and fetch all projects data/

requst URL: https://dev.azure.com/TopTeamIntegration/_apis/projects

response JSON:  (we get all project details. fields we get: )
count: 1 // number of projects 
  {
      "id": "b473305b-d63e-451f-9455-b289ab029261", //id of proj 1
      "name": "TEMP", // project name 
     "url": "https://dev.azure.com/TopTeamIntegration/_apis/projects/b473305b-d63e-451f-9455-b289ab029261", //project url 
      "state": "wellFormed", //state of project
      "revision": 19,	//revision num of project 
      "visibility": "private", // visibility of project
      "lastUpdateTime": "2020-06-30T13:27:36.16Z"
    },