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


# PuppyTracker - Original readme:

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 8.3.24.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
