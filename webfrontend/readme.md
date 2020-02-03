# Technical Description of Web Frontend
Following the steps from [This blogpost](https://dev.to/dileno/build-an-angular-8-app-with-rest-api-and-asp-net-core-2-2-part-2-46ap) to create an Angular frontend backed by a .Net Core WebAPI. The remainder of this document are the steps that I've taken to reach something useable. 

## Prerequisites

### Install NodeJS and Angular support
- Install Node.js
- Install Angular CLI

If you can't figure out how to install NodeJS, then stop reading now, and go seek medical advice :)

To install the Angular CLI perform the following conjurations in a terminal window: 

```
npm install -g @angular/cli
```

Once performed, you can verify that it's ok by typing
```
ng --version
```
Which should bring up some fancy ASCII art.


### Install Visual Studio Code and some handy extensions
- Install Angular Snippets for VS Code
- Debugger for Chrome
- TSLint
- Beautify for VSCode
- Path Intellisense


## Setting up the projects

### Angular
Navigate to your projects folder and hit the following command to create an Angular Project for your puppy tracker: 

```
ng new PuppyTracker
```
This will create a subfolder named `PuppyTracker` and populate it with all sorts of fun. Make sure to select **yes** for using routing, and choose **SCSS**  for your style format.


