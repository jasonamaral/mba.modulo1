﻿namespace MBA.Modulo1.Blog.Domain.DTO;

public class JwtSettings
{
    public string? Secret { get; set; }
    public int ExpireinHours { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}