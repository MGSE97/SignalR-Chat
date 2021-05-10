# SignalR Chat

SignalR Chat demo with authorization using Identity Server. 
Written in ASP.NET Core 3.1, MVC and JS. 
Based on [Tutorial: Get started with ASP.NET Core SignalR](https://docs.microsoft.com/en-gb/aspnet/core/tutorials/signalr?tabs=visual-studio&view=aspnetcore-3.1) and extended with user management and private messages.

## Getting Started

Clone this repository, and open solution using Visual Studio 2019 or later.
Then Build and Run the SignalR project.

### Prerequisites

- Visual Studio 2019
- ASP.NET Core 3.1

### Solution

Projects:
- ChatCore - Complete chat logic
- SignalR - Website presentation

#### Chat

All chat logic has moved to ChatCore project.
This way is simpler to change Presentation Layer (Website).
It`s bit more complicated, but all methods and classes are documented.

Still uses simplest in memory storage methods for users.

Interesting paths:
```
~/Startup.cs
~/Hubs/ChatHub.cs
~/Services/ChatService.cs
~/Services/MessageHandlers
```

#### Website

Website is simple MVC template with In-application Authorization.

To use chat log-in as user, then Chat tab will appear at main menu.
Navigate to Chat view and send messages.

It's probably better to have multiple clients connected, you can use privacy mode or other browsers.
                  

Interesting files:
```
~/Startup.cs
~/wwwroot/js/chat.js
~/wwwroot/lib/microsoft/signalr
~/Views/Home/Chat.cshtml
```

### Features

- User accounts
- SignalR chat                 
  * Private messages `/pm: username message`
  * User list `/users`                  
  * Connect messages
                               
## Built With

* [ASP.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core) - Framework
* [Visual Studio 2019](https://visualstudio.microsoft.com/cs/vs/) - IDE
* [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) - Real-time communication lib
* [Identity Server](https://github.com/IdentityServer/IdentityServer4) - Authorization, authentification and account management

## Author

* [**MGSE97**](https://github.com/MGSE97)

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details

## Donate

Found this project useful or want to buy me a 🍺, ☕, 🍵. Consider donating using buttons bellow.

[![Donate PayPal](https://img.shields.io/badge/Donate-PayPal-blue.svg)](https://www.paypal.com/donate?hosted_button_id=HTDSBL37M2PR4&source=url)
[![Donate Crypto](https://img.shields.io/badge/Donate-Crypto-yellow.svg)](https://commerce.coinbase.com/checkout/ab045b90-3fc5-4dc6-8310-b51e2617a5e4)
