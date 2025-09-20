namespace MartianMike;

using Chickensoft.GoDotTest;
using Chickensoft.GodotTestDriver;
using Chickensoft.GodotTestDriver.Drivers;
using Godot;
using Shouldly;
using System.Threading.Tasks;

public class GameTest : TestClass
{
    private Game _game = default!;
    private Fixture _fixture = default!;

    public GameTest(Node testScene) : base(testScene) { }

    [SetupAll]
    public async Task Setup()
    {
        _fixture = new Fixture(TestScene.GetTree());
        _game = await _fixture.LoadAndAddScene<Game>();
    }

    [CleanupAll]
    public void Cleanup() => _fixture.Cleanup();

    [Test]
    public void TestButtonUpdatesCounter()
    {
        var buttonDriver = new ButtonDriver(() => _game.StartButton);
        buttonDriver.ClickCenter();
        _game.ButtonPresses.ShouldBe(1);
    }
}
