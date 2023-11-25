import React, {useState} from "react";
import Map from "./Map";
import { FormThemeProvider } from 'react-form-component'
import MainForm from './MainForm'

function App() {
  const [pathData, setPathData] = useState();

  return (
    <FormThemeProvider>
      <MainForm pathData={pathData} setPathData={setPathData} />
      <Map pathData={pathData} />
    </FormThemeProvider>
  );
}

export default App;
