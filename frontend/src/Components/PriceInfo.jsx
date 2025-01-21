import React from "react";
import styled from "styled-components";

const PriceInfo = ({ priceData }) => {
    return (
        <PriceInfoContainer>
            <InfoItem>
                <strong>Genomsnitt</strong> <br /> {priceData.average} SEK/kwh
            </InfoItem>
            <InfoItem>
                <strong>Högsta</strong> <br />{priceData.highestPrice} SEK/kwh
                <div>Tid: {priceData.highestTime}</div>
                <div>Procentuell ökning mot genomsnitt: {priceData.highestDiff}%</div>
            </InfoItem>
            <InfoItem>
                <strong>Lägsta</strong><br />
                {priceData.lowestPrice} SEK/kWh
                <div>Tid: {priceData.lowestTime}</div>
                <div>Procentuell sänkning mot genomsnitt: {priceData.lowestDiff}%</div>
            </InfoItem>
        </PriceInfoContainer>
    );
};

export default PriceInfo;

const PriceInfoContainer = styled.div`
  margin-top: 20px;
  color: #444;
  text-align: left;
`;

const InfoItem = styled.div`
  margin: 10px 0;
  font-size: 1.2rem;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 10px;
  box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.1);
  strong {
    color: #2d87f0;
  }
`;