﻿namespace Web.Api.UI.Models;

public class DatabaseSetting
{
    public string ConnectionString { get; set; } = string.Empty;
	public string DatabaseName { get; set; } = string.Empty;
	public string CollectionName { get; set; } = string.Empty;
}