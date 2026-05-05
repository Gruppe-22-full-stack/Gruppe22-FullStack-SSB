using StatistikkApp.Models;
using Xunit;

namespace StatistikkApp.Tests;

public class StatistikkDataTests
{
    [Fact]
    public void StatistikkData_CanStoreValues()
    {
        var data = new StatistikkData
        {
            Aar = 2024,
            Verdi = 717710,
            KommuneId = 1,
            StatistikkKategoriId = 1
        };

        Assert.Equal(2024, data.Aar);
        Assert.Equal(717710, data.Verdi);
        Assert.Equal(1, data.KommuneId);
        Assert.Equal(1, data.StatistikkKategoriId);
    }
}