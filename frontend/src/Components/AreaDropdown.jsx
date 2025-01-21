import React from "react";
import styled from "styled-components";

const areas = {
    "Norra Sverige": "SE1",
    "Norra Mellansverige": "SE2",
    "Södra Mellansverige": "SE3",
    "Södra Sverige": "SE4"
};

const AreaDropdown = ({ selectedArea, setSelectedArea }) => {
    return (
        <Dropdown>
            <select value={selectedArea} onChange={(e) => setSelectedArea(e.target.value)}>
                {Object.keys(areas).map((area) => (
                    <option key={area} value={areas[area]}>
                        {area}
                    </option>
                ))}
            </select>
        </Dropdown>
    );
};

export default AreaDropdown;

const Dropdown = styled.div`
margin: 20px 0;
select {
padding: 10px
font-size: 1.2rem;
background-color: #e0e0e0;
border-radius: 5px;
border:none;
box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.2);
    }
`;