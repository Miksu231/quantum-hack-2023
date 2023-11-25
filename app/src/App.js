import React, {useState} from "react";
import Map from "./Map";

function App() {

  const [routeType, setRoute] = useState("fastest");

  return <div>
      <button onClick={() => setRoute("fastest")}>Fastest route</button>
      <button onClick={() => setRoute("cheapest")}>Cheapest route</button>
      <Map routeType={routeType} />
    </div>;
}

export default App;
