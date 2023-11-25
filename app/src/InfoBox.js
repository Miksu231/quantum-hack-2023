import React from "react";
import "./styles/InfoBox.css";

const URL = 'http://localhost:5000/quantumHack';

const InfoBox = ({pathData}) => {
    return(
        <div className="info-box">
            <p><b>Cost:</b> {pathData ? pathData.cost: '-'}</p>
            <p><b>Emissions:</b> {pathData ? pathData.emissions: '-'}</p>
            <p><b>Time:</b> {pathData ? pathData.time: '-'}</p>
        </div>
    )
}

export default InfoBox