import React from "react";
import "./styles/MainForm.css";
import Form, {
    Select,
} from 'react-form-component'
import InfoBox from "./InfoBox";

const URL = 'http://localhost:5000/quantumHack';

const MainForm = ({pathData, setPathData}) => {
    const  handleChange = (event) => {
        if (event === 'All') {
            getPath('Balanced');
        } else {
            getPath(event);
        }
	};

    const getPath = async (routeType) => {
        const pathData = await fetch(URL, {method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify({ type: routeType })});
        const data = await pathData.json();
        setPathData(data);
    };

    return (
        <Form className="main-form" fields={['type']}>
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