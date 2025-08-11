# LolFetch
LolFetch is a terminal tool to fetch and render an ASCII art of league of legends champions. It relies on data dragon api provided by Riot Games to download champion splash arts and also in ASCII escape codes to properly render the image inside the terminal correctly.

## How it Works
It uses the `System.CommandLine` to handle and parse the command line arguments. Running the command line program with `--help` shows the message below:
```c#
Description:
  Render your favorite splash art of League of Legends champions in ASCII

Usage:
  LolFetch.Terminal <champion> [options]

Arguments:
  <champion>  champion to be rendered in ASCII

Options:
  -?, -h, --help  Show help and usage information
  --version       Show version information
  --color         Color image of champion instead of grayscale
  --square        Square image of champion instead of splash art
```

- The default behavior is looking for the champion, downloading it and rendering using gray color values calculated from the original RGB pixels.
- If the color option is passed, then it does the same, but it uses ASCII escape codes to specify color following the original pixels.
- If the `--square` option is passed, then it downloads the square image of the champion instead of the default one.

All images are resized to fit the image inside the current terminal conditions (width x height).