import React, { useState } from 'react';
import DatePicker from "react-datepicker";

export function GtDateTimePicker(props) {
    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(null);
    
    const onChange = date => props.onSelectDate(date);
    return (
        <DatePicker
            selected={startDate}
            onChange={onChange}
            startDate={startDate}
            endDate={endDate}
            inline
        />
    );
}
