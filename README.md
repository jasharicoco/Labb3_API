# Labb3_API

## GET /interests
### Hämtar en lista med alla intressen.

Exempel på svar:
  {
    "id": 0,
    "name": "string",
    "description": "string",
    "persons": [
      {
        "name": "string"
      }
    ],
    "links": [
      {
        "id": 0,
        "url": "string",
        "description": "string"
      }
      

## POST /interests
### Skapar ett nytt intresse.

Exempel på begäran:
{
  "interestName": "string",
  "description": "string",
  "linkIds": [0],
  "personIds": [0]
}

  
## GET /links
### Hämtar en lista med alla länkar.

Exempel på svar:
  {
    "id": 0,
    "url": "string",
    "description": "string",
    "interests": [
      {
        "name": "string",
        "description": "string"
      }
    ],
    "persons": [
      {
        "name": "string"
      }
    ]
  
  
## POST /links
### Skapar en ny länk.
  
Exempel på begäran:
{
  "url": "string",
  "description": "string",
  "interestIds": [0],
  "personIds": [0]
}
  
  
## GET /persons
### Hämtar en lista med alla personer.
  
Exempel på svar:
  {
    "id": 0,
    "name": "string",
    "email": "user@example.com",
    "telephone": "string",
    "interests": [
      {
        "name": "string",
        "description": "string",
        "links": [
          {
            "url": "string",
            "description": "string"
          }
        
  
  
## POST /persons
### Skapar en ny person.
  
Exempel på begäran:
{
  "name": "string",
  "email": "user@example.com",
  "telephone": "string",
  "interestIds": [0],
  "linkIds": [0]
}


## PUT /persons/{personId}/interest/{interestId}
### Kopplar ett befintligt intresse till en specifik person.

Exempel på svar:
"{interestName} har lagts till för {personName}"

## GET /persons/{personId}/interests
### Hämtar alla intressen kopplade till en specifik person.

Exempel på svar:
[
  {
    "name": "string",
    "description": "string"
  }
]


## GET /persons/{personId}/links
### Hämtar alla länkar kopplade till en specifik person.

Exempel på svar:
[
  {
    "name": "string",
    "url": "string",
    "description": "string"
  }
]


## POST /persons/{personId}/interest/{interestId}/link
### Lägger till en ny länk för en specifik person och ett specifikt intresse.

Exempel på begäran:
{
  "url": "string",
  "description": "string"
}

Exempel på svar:
"Ny länk har lagts till för {personName} under intresset '{interestName}'"
