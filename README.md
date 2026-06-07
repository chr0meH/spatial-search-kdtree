# Spatial Search (Geospatial KD-Tree)

This repository contains a highly optimized C# implementation of a K-Dimensional Tree (KD-Tree), specifically tailored for fast geospatial queries using latitude and longitude coordinates. 

The algorithm is designed to efficiently index geographical points and perform rapid radius-based searches, making it ideal for location-based services, mapping applications, or nearest-neighbor problems.

## 🚀 Key Features & Optimizations

* **Geospatial Radius Search:** Efficiently finds all points within a specified radius (in kilometers) from a target location using the **Haversine formula** for accurate spherical distance calculations.
* **Bounding Box (AABB) Pruning:** Each node calculates and stores its spatial boundaries. During a search, the algorithm dynamically converts the search radius into a geographical bounding box, instantly discarding entire tree branches that do not intersect.
* **Leaf Node Bucketing:** To optimize memory usage and CPU cache performance, the tree utilizes a bucketing strategy (`MaxPointsInLeaf = 50`). This prevents excessive recursion depth and accelerates the final distance evaluations.
* **Median Splitting:** The tree is dynamically balanced during construction by recursively sorting and splitting the data sets along the median of alternating axes (Longitude and Latitude).

## 🛠️ Tech Stack & Skills Highlighted

* **Language:** C#
* **Core Concepts:** Advanced Data Structures (Trees, Spatial Partitioning), Algorithmic Optimization (Recursion, Branch Pruning), Geospatial Mathematics (Haversine formula), LINQ.
