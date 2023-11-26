import React, { useRef, useEffect, useState } from "react";
import mapboxgl from "mapbox-gl";
import "./styles/Map.css";

mapboxgl.accessToken = "pk.eyJ1IjoidmFyaG9sYSIsImEiOiJjbHBlN3J0N2wxMnF1Mmpwcmd5OTIxajAxIn0.S2DU5ZBFpRzNoFQwZ7JJqw"
// "pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4M29iazA2Z2gycXA4N2pmbDZmangifQ.-g_vE53SD2WrJ6tFX7QHmA";

const URL = 'http://localhost:5000/quantumHack/';

const Map = ({ pathData, area }) => {
  const mapContainerRef = useRef(null);

  const [graphData, setGraphData] = useState();

  const getData = async () => {
    const nodeData = await fetch(URL + `${area}/`)
    const data = await nodeData.json();
    setGraphData(data);
  };

  // GET basic node data
  useEffect(() => {
    getData();
  }, [area]);

  // Initialize map when component mounts
  useEffect(() => {

    const map = new mapboxgl.Map({
      container: mapContainerRef.current,
      style: "mapbox://styles/mapbox/satellite-v9",
      center: [0, 0],
      zoom: 1,
    });

    let vertices = {};

    if (graphData !== undefined) {
      let verticeArray = graphData.vertices;

      // starting node
      const startCoords = [graphData.startPoint.longitude, graphData.startPoint.latitude];
      vertices[graphData.startPoint.id] = startCoords;
      verticeArray.push(graphData.startPoint);
      let startPopup = new mapboxgl.Popup().setText(`${graphData.startPoint.name}`).addTo(map)
      new mapboxgl.Marker().setLngLat(startCoords).setPopup(startPopup).addTo(map);
      // endpoint
      const endCoords = [graphData.endPoint.longitude, graphData.endPoint.latitude];
      vertices[graphData.endPoint.id] = endCoords;
      verticeArray.push(graphData.endPoint);
      let endPopup = new mapboxgl.Popup().setText(`${graphData.endPoint.name}`).addTo(map)
      new mapboxgl.Marker().setLngLat(endCoords).setPopup(endPopup).addTo(map);
      // rest of the nodes
      graphData.vertices.forEach(vertice => {
        const coords = [vertice.longitude, vertice.latitude];
        vertices[vertice.id] = coords;
        let popup = new mapboxgl.Popup().setText(`${vertice.name}`).addTo(map)
        new mapboxgl.Marker().setLngLat(coords).setPopup(popup).addTo(map);
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

    if (graphData !== undefined && pathData !== undefined) {
      console.log(pathData);
      map.on('load', () => {
        pathData.path.forEach((step, i) => {
          const road = [vertices[step.destinationId], vertices[step.originId]];
          map.addSource(`step-${i}`, {
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
            'id': `step-${i}`,
            'type': 'line',
            'source': `step-${i}`,
            'layout': {
              'line-join': 'round',
              'line-cap': 'round'
            },
            'paint': {
              'line-color': '#448',
              'line-width': 8
            }
          });
        });
      });
    };

    // Add navigation control (the +/- zoom buttons)
    map.addControl(new mapboxgl.NavigationControl(), "top-right");

    // Clean up on unmount
    return () => map.remove();
  }, [pathData, graphData]);

  return(
    <div>
      <div className="map-container" ref={mapContainerRef} />
    </div>
  );
};

export default Map;
