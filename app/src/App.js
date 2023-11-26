import React, {useState} from "react";
import Map from "./Map";
import { FormThemeProvider } from 'react-form-component'
import MainForm from './MainForm'

function App() {
  const [pathData, setPathData] = useState();
  const [area, setArea] = useState("Europe");

  return (
    <FormThemeProvider>
      <MainForm pathData={pathData} setPathData={setPathData} setArea={setArea} area={area} />
      <Map pathData={pathData} area={area} />
    </FormThemeProvider>
  );
}

export default App;
