# Lucide Icon Tag Helper for ASP.NET

A simple Razor tag helper that renders [Lucide icons](https://lucide.dev) as inline SVG in your ASP.NET applications.

## Installation

This is not a NuGet package. To use it in your project:

1. Copy `LucideIconTagHelper.cs` and `LucideIconsDictionary.cs` into your ASP.NET project
2. Update the namespace in both files to match your project's namespace (or keep `EstusLabs.Lucide` if you prefer)
3. Register the tag helper in your `_ViewImports.cshtml` file

### Registering the Tag Helper

Add the following line to your `_ViewImports.cshtml` file (typically located in `Views/` or `Pages/`):

```razor
@addTagHelper *, YourAssemblyName
```

Replace `YourAssemblyName` with your actual assembly name. For example:

```razor
@addTagHelper *, MyWebApp
```

If you changed the namespace from `EstusLabs.Lucide`, make sure the namespace in both files matches.

## Usage

Use the `<lucide-icon>` tag helper in your Razor views or pages:

```razor
<lucide-icon name="heart" />
```

### Attributes

- **`name`** (required): The name of the Lucide icon (case-insensitive). The `.svg` extension is optional.
  ```razor
  <lucide-icon name="heart" />
  <lucide-icon name="user-circle" />
  <lucide-icon name="arrow-right.svg" /> <!-- .svg extension is optional -->
  ```

- **`class`** (optional): CSS classes to apply to the SVG element.
  ```razor
  <lucide-icon name="star" class="text-yellow-500" />
  <lucide-icon name="settings" class="icon-large text-blue-500" />
  ```

- **`size`** (optional): Sets both `width` and `height` attributes on the SVG. Can be any valid CSS size value.
  ```razor
  <lucide-icon name="home" size="24" />
  <lucide-icon name="search" size="1.5rem" />
  <lucide-icon name="menu" size="32px" />
  ```

### Examples

Basic usage:
```razor
<lucide-icon name="home" />
```

With custom styling:
```razor
<lucide-icon name="user" class="text-blue-600" size="32" />
```

In a button:
```razor
<button class="btn">
    <lucide-icon name="download" size="20" />
    Download
</button>
```

In a link:
```razor
<a href="/settings">
    <lucide-icon name="settings" class="icon" />
    Settings
</a>
```

## Available Icons

All icons from the Lucide icon set are available. Icon names use kebab-case (e.g., `user-circle`, `arrow-right`, `file-text`).

Visit [lucide.dev](https://lucide.dev/icons) to browse all available icons. Use the icon name exactly as shown on the website (case-insensitive).

## How It Works

- Icons are pre-compiled into a static dictionary (`LucideIconsDictionary`) for fast O(1) lookups
- The tag helper renders the SVG inline, so no external requests are needed
- Icons use `currentColor` for the stroke, so they inherit the text color from their parent element
- If an icon name is not found or invalid, the tag helper suppresses output (renders nothing)

## Notes

- Icon names are case-insensitive and automatically normalized to lowercase
- The `.svg` extension in icon names is optional and will be stripped automatically
- Icons inherit the current text color via `stroke="currentColor"`, making them easy to style with CSS
- If you need to update the icon set, you'll need to regenerate `LucideIconsDictionary.cs` using the provided generation scripts
