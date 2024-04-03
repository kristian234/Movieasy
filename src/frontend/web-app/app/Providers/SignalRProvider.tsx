'use client'

import { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import { Movie } from '@/types';
import { toast } from 'react-toastify';

interface Props{
    jwt: string
}

export default function SignalRComponent({jwt} : Props) {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);

    const handleNewMovieRelease = (movie: Movie) => {
        toast("NEW MOVIE NEW MOVIE");
    };

    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:5001/notificationHub', {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets,
                accessTokenFactory() {
                    return jwt;
                },
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, [])
    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => {
                    connection.on("ReceiveNewMovieRelease", handleNewMovieRelease);
                }).catch(error => console.log(error))
        }

        return () => {
            connection?.stop()
        }
    }, [connection])

    return null;
}
