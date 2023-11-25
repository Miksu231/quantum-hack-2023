import React, { useRef, useEffect, useState } from "react";
import mapboxgl from "mapbox-gl";
import "./styles/Map.css";

mapboxgl.accessToken =
  "pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4M29iazA2Z2gycXA4N2pmbDZmangifQ.-g_vE53SD2WrJ6tFX7QHmA";

const URL = 'http://localhost:5000/quantumHack';

const Map = ({ routeType }) => {
  const mapContainerRef = useRef(null);

  const [graphData, setGraphData] = useState();

  const getData = async () => {
    const nodeData = await fetch(URL)
    const data = await nodeData.json();
    setGraphData(data);
  };

  // GET basic node data
  useEffect(() => {
    getData();
  }, []);

  // Initialize map when component mounts
  useEffect(() => {

    const map = new mapboxgl.Map({
      container: mapContainerRef.current,
      style: "mapbox://styles/mapbox/streets-v11",
      center: [0, 0],
      zoom: 10,
    });

    if (graphData !== undefined) {

      let vertices = {};
      let verticeArray = graphData.vertices;

      // starting node
      const startCoords = [graphData.startPoint.latitude, graphData.startPoint.longitude];
      vertices[graphData.startPoint.id] = startCoords;
      verticeArray.push(graphData.startPoint);
      new mapboxgl.Marker().setLngLat(startCoords).addTo(map);
      // endpoint
      const endCoords = [graphData.endPoint.latitude, graphData.endPoint.longitude];
      vertices[graphData.endPoint.id] = endCoords;
      verticeArray.push(graphData.endPoint);
      new mapboxgl.Marker().setLngLat(endCoords).addTo(map);
      // rest of the nodes
      graphData.vertices.forEach(vertice => {
        const coords = [vertice.latitude, vertice.longitude];
        vertices[vertice.id] = coords;
        new mapboxgl.Marker().setLngLat(coords).addTo(map);
      });
      // draw edges
      map.on('load', () => {
        verticeArray.forEach((vertice, i) => {
          vertice.edges.forEach((edge, j) => {
            const road = [vertices[edge.originId], vertices[edge.destinationId]];
            map.addSource(`route-${i}-${j}`, {
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
              'id': `route-${i}-${j}`,
              'type': 'line',
              'source': `route-${i}-${j}`,
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
      });
    }

    // Add navigation control (the +/- zoom buttons)
    map.addControl(new mapboxgl.NavigationControl(), "top-right");

    // Clean up on unmount
    return () => map.remove();
  }, [routeType, graphData]);

  return <div className="map-container" ref={mapContainerRef} />;
};

export default Map;
