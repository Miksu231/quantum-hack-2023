import React, { useRef, useEffect } from "react";
import mapboxgl from "mapbox-gl";
import "./styles/Map.css";
import nodes from "./data/nodes.json";
import roads from "./data/roads.json";
import fastestPath from "./data/roads1.json";
import cheapestPath from "./data/roads2.json"

mapboxgl.accessToken =
  "pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4M29iazA2Z2gycXA4N2pmbDZmangifQ.-g_vE53SD2WrJ6tFX7QHmA";

const Map = ({routeType}) => {
  const mapContainerRef = useRef(null);

  // Initialize map when component mounts
  useEffect(() => {

    const map = new mapboxgl.Map({
      container: mapContainerRef.current,
      style: "mapbox://styles/mapbox/streets-v11",
      center: [-87.65, 41.84],
      zoom: 10,
    });

    map.on('load', () => {

      let path = roads.roads;

      if (routeType === "fastest") {
       path = fastestPath.roads;
      } else if (routeType === "cheapest") {
        path = cheapestPath.roads;
      }

      path.forEach((road, i) => {

        map.addSource(`route-${i}`, {
          'type': 'geojson',
          'data': {
            'type': 'Feature',
            'properties': {},
            'geometry': {
              'type': 'LineString',
              'coordinates': road
            }
          }
        });

        map.addLayer({
          'id': `route-${i}`,
          'type': 'line',
          'source': `route-${i}`,
          'layout': {
            'line-join': 'round',
            'line-cap': 'round'
          },
          'paint': {
            'line-color': '#844',
            'line-width': 8
          }
        });

      });
      
    });

    // Create nodes
    nodes.coords.map((coords) =>
      new mapboxgl.Marker().setLngLat(coords).addTo(map)
    );

    // Add navigation control (the +/- zoom buttons)
    map.addControl(new mapboxgl.NavigationControl(), "top-right");

    // Clean up on unmount
    return () => map.remove();
  }, [routeType]);

  return <div className="map-container" ref={mapContainerRef} />;
};

export default Map;
