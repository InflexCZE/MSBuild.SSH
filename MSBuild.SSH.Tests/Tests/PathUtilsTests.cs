using MSBuild.SSH.Utils;

namespace MSBuilder.SSH.Tests.Tests;

public class PathUtilsTests
{
    [Fact]
    public void RootPath()
    {
        Assert.False(PathUtils.IsRootedPath("."));
        Assert.True(PathUtils.IsRootedPath("/home/pi"));
        Assert.True(PathUtils.IsRootedPath("/home/pi/"));
    }

    [Fact]
    public void CanonizePath()
    {
	    var homePath = "/home/pi";
	    Assert.Equal($"{homePath}", PathUtils.CanonizePath("", homePath));
	    Assert.Equal($"{homePath}", PathUtils.CanonizePath(".", homePath));
	    Assert.Equal($"{homePath}", PathUtils.CanonizePath(null, homePath));
	    Assert.Equal($"{homePath}/a/b", PathUtils.CanonizePath("a/b", homePath));
	    Assert.Equal($"{homePath}/a/b", PathUtils.CanonizePath("a\\b", homePath));
	    Assert.Equal($"{homePath}/a/b", PathUtils.CanonizePath("./a\\b", homePath));
	    Assert.Equal($"{homePath}/a/b", PathUtils.CanonizePath(".\\a\\b", homePath));
	    
	    Assert.Equal($"/a/b", PathUtils.CanonizePath("/a/b", homePath));
	    Assert.Equal($"/a/b", PathUtils.CanonizePath("/a\\b", homePath));
    }

	[Fact]
    public void IncrementalPathSegments()
    {
	    AssertSegments("a/b", "a/", "a/b/");
	    AssertSegments("./a/b", "a/", "a/b/");
	    AssertSegments("/a/b", "/a/", "/a/b/");

	    void AssertSegments(string path, params string[] expected)
	    {
		    var actual = PathUtils.IncrementalPathSegments(path).ToArray();
            Assert.Equal(expected, actual);
	    }
    }
}