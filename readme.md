# Puppy Tracker v1.0
A successfull puppy means a puppy that is settled with routine and consistent feeding and toilet times. 

This project is all about tracking your puppy's logistics, feeding, pooping and peeing and crate training times to make it easier to learn the statistics of YOUR puppy. 

## Purpose
The purpose of this project is to deploy it to a Raspberry Pi 4 with a touch-screen. 
It will serve as a data-gathering point located close to the puppy's crate. Backed by an Asp.Net core backend, the data is gathered and posted to an Azure Storage table for reporting in Excel and/or PowerBI. 

## Implementation
The WebApi builds on a standard *domain-driven design* principle.
The solution has two main components in the presentation layer: 

- A standard ASP.Net Core WebApi, aptly named `PuppyApi` for which all CRUD operations and server-side logic is performed
- A `PuppyTrackerClient` project, written in Microsoft Asp.Net Blazor Web-hosted components environment for providing a rich, SPA environment. 

## Installation / Requirements
In order to successfully run this code, you'll need to set up an `Azure Storage Account` and an `Application Insights Instance`. The solution uses Azure Table Storage as it's main repository, and tracks telemetry using the insights instance.

Add the following secrets to your configuration: 

```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "<your Application Insights instrumentation key>"
  },
  "AzureStorage": {
    "ConnectionString": "<full connection string to your Azure Storage account>"
  }
}
```







