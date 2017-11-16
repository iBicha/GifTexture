# GifTexture
Simple GifTexture for Unity. Play gifs on a textures, and render to any Render component.
This component only plays gifs, from a file or from a byte[] buffer.
Based on http://giflib.codeplex.com/ where bitmap work was replaced by textures. Unsafe code also removed.
Memory consideration: GifTexture will keep one texture only in graphics memory, but will have all the frames as a Color32[] array in your RAM.
