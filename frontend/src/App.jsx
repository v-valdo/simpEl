import React, { useState, useEffect } from 'react';
import styled from 'styled-components';
import AreaDropdown from './Components/AreaDropdown';
import PriceInfo from './Components/PriceInfo';

const App = () => {
  const [selectedArea, setSelectedArea] = useState('SE1');
  const [priceData, setPriceData] = useState(null);

  useEffect(() => {
    const fetchPriceData = async () => {
      const response = await fetch(`http://localhost:5184/api/priceData/` + selectedArea);
      const data = await response.json();
      console.log(data);
      setPriceData(data);
    };

    fetchPriceData();
  }, [selectedArea]);
  return (
    <Container>
      <Title>SimpEl</Title>
      <AreaDropdown selectedArea={selectedArea} setSelectedArea={setSelectedArea} />
      {priceData && <PriceInfo priceData={priceData} />}
    </Container>
  );
};

export default App;

const Container = styled.div`
  font-family: 'Roboto', Courier, monospace;
  text-align: center;
  margin: 50px;
  background-color:rgb(209, 249, 225);
  border-radius: 10px;
  padding: 30px;
`;

const Title = styled.h1`
  color: #555;
  font-size: 3rem;
  text-shadow: 1px 1px 2px #000;
`;