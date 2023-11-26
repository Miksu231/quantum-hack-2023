import React, {useState} from "react";
import "./styles/MainForm.css";
import Form, {
    Select,
} from 'react-form-component'
import InfoBox from "./InfoBox";

const URL = 'http://localhost:5000/quantumHack/';

const MainForm = ({pathData, setPathData, setArea, area}) => {

    const [type, setType] = useState("Balanced");

    const  handleChange = (event) => {
        if (event === 'All') {
            setType('Balanced');
        } else {
            setType(event);
        }
        getPath()
	};

    const  handleAreaChange = (event) => {
        setArea(event);
        getPath()
	};

    const getPath = async () => {
        const pathData = await fetch(URL + `${area}/`, {method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify({ type: type, demand: 100.0 })});
        const data = await pathData.json();
        setPathData(data);
    };

    return (
        <Form className="main-form" fields={['type']}>
            <Select
                onChange={handleAreaChange}
                name='type'
                label='Business area'
                options={['Asia', 'Europe', 'NorthAmerica']}
            />
            <Select
                onChange={handleChange}
                name='type'
                label='Shortest path based on'
                options={['Emission', 'Cost', 'Time']}
            />
            <InfoBox pathData={pathData}></InfoBox>
        </Form>
    )
};

export default MainForm;