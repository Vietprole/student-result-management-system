using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Student_Result_Management_System.Data;
using Microsoft.AspNetCore.Mvc.Testing;

namespace StudentResultManagementSystem.Tests;

public class ServiceTest : IClassFixture<WebApplicationFactory<Program>> {

    private readonly WebApplicationFactory<Program> _factory;

    public ServiceTest(WebApplicationFactory<Program> factory){
        _factory = factory;
    }

    [Fact]
    public void Test1(){
        //TODO: TEST CODE
    }
}