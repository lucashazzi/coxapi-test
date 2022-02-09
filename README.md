### Hello and Welcome
#### This is my .Net WebAPI for COX Automotive test.

##### I had some problems publishing the API, therefore i will document it in here.

 The API works on a Swagger interface, so to test the calls, you must download the source code and build it. A new browser tab will appear with a localhost address. Simply add "/swagger"
 after the address and the GET requests should appear.


#### The Requests:
The API have 3 different sources of information. You can get **photos**, **users** and **albums**.

For each source, you can get all the information at once or filter it in various ways:

To get all the information at once, use only the "/{infoSource}" to make a request (E.g. "/Photo").

Obs.: The GET ALL of Photo is limited to a thousand records for sake of: "I dont know how to optimize the List<Object> returns".

To filter, use "/{infoSource}/{filterType}/{filter}" (E.g. "/Photo/id/51"). This is the default for all filters.

When using a filter that is a String (like a /title filter) the return will contain all records that contains the text inserted. E.g. "/Photo/title/officiis" will return more than one value:

```
 {
    "albumId": 1,
    "id": 8,
    "title": "aut porro officiis laborum odit ea laudantium corporis",
    "url": "https://via.placeholder.com/600/54176f",
    "thumbnailUrl": "https://via.placeholder.com/150/54176f"
  },
  {
    "albumId": 1,
    "id": 24,
    "title": "beatae officiis ut aut",
    "url": "https://via.placeholder.com/600/8f209a",
    "thumbnailUrl": "https://via.placeholder.com/150/8f209a"
  },
  {
    "albumId": 2,
    "id": 53,
    "title": "soluta et harum aliquid officiis ab omnis consequatur",
    "url": "https://via.placeholder.com/600/6efc5f",
    "thumbnailUrl": "https://via.placeholder.com/150/6efc5f"
  },
			     [. . .]
```

_________________________________

#### Thats it!
I know that you probably expected more, are a bit disappointed. And even if i don't pass, i want you to know that in the last week i've learnt basically everything that is in this code. I've learnt the basics of creating a WebAPI with .Net, http resiliency (the basics, tried using Polly but time was not forgiving), the concepts of caching and web requests itself! Thank you for the opportunity, and i expect to see you in the near (or not so near) future!
