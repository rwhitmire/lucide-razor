#!/bin/bash

# Script to download all Lucide icons and save them to an "icons" folder

set -e  # Exit on error

TARGET_DIR="icons"
REPO_URL="https://github.com/lucide-icons/lucide.git"
TEMP_DIR="lucide-temp"

# Colors for output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

echo -e "${BLUE}Downloading Lucide icons...${NC}"

# Create target directory if it doesn't exist
mkdir -p "$TARGET_DIR"

# Clean up any existing temp directory
if [ -d "$TEMP_DIR" ]; then
    echo "Cleaning up existing temp directory..."
    rm -rf "$TEMP_DIR"
fi

# Clone the repository (shallow clone for faster download)
echo "Cloning Lucide repository..."
git clone --depth 1 "$REPO_URL" "$TEMP_DIR"

# Check if icons directory exists in the cloned repo
if [ ! -d "$TEMP_DIR/icons" ]; then
    echo "Error: Icons directory not found in repository"
    rm -rf "$TEMP_DIR"
    exit 1
fi

# Copy all SVG icons to the target directory
echo "Copying icons..."
cp "$TEMP_DIR/icons"/*.svg "$TARGET_DIR/" 2>/dev/null || {
    echo "Warning: No SVG files found in icons directory"
}

# Count the number of icons downloaded
ICON_COUNT=$(ls -1 "$TARGET_DIR"/*.svg 2>/dev/null | wc -l)

# Clean up the temporary directory
echo "Cleaning up..."
rm -rf "$TEMP_DIR"

if [ "$ICON_COUNT" -gt 0 ]; then
    echo -e "${GREEN}Successfully downloaded $ICON_COUNT icons to '$TARGET_DIR' directory.${NC}"
else
    echo "Warning: No icons were downloaded."
    exit 1
fi