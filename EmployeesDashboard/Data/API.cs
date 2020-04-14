using System;

namespace EmployeesDashboard.Data {
  public class API {
    private static string API_URL {
      get {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower() == "development")
          return "https://localhost:6001/api/";

        return "https://core-2.azurewebsites.net/api/";
      }
    }

    public static string URL() {
      return API_URL;
    }

    public static string URL(string endpoint) {
      return API_URL + endpoint;
    }
  }
}