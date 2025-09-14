import React, { useState, useEffect } from "react";
import {
    Container,
    TextField,
    Button,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    Typography,
    Paper,
    Box,
    Grid,
    Card,
    CardContent,
} from "@mui/material";

export default function Calculator() {
    const [n, setN] = useState("");
    const [m, setM] = useState("");
    const [p, setP] = useState("");
    const [matrix, setMatrix] = useState([]);
    const [result, setResult] = useState(null);
    const [history, setHistory] = useState([]);
    const [selectedIndex, setSelectedIndex] = useState(null);
    const [isNewCalculate, setIsNewCalculate] = useState(true);

    // Load history on mount
    useEffect(() => {
        handleGetHistory();
    }, []);

    // Auto-generate matrix when n or m changes
    useEffect(() => {
        if (!n || !m) {
            setMatrix([]);
            return;
        }

        const numN = Number(n);
        const numM = Number(m);

        if (numN > 0 && numM > 0) {
            setMatrix((prev) => {
                const newMatrix = Array.from({ length: numN }, (_, i) =>
                    Array.from({ length: numM }, (_, j) =>
                        prev[i] && prev[i][j] ? prev[i][j] : 1
                    )
                );
                return newMatrix;
            });
        }
    }, [n, m]);

    const handleMatrixChange = (i, j, value) => {
        const newMatrix = matrix.map((row, ri) =>
            row.map((cell, ci) => (ri === i && ci === j ? Number(value) : cell))
        );
        setMatrix(newMatrix);
        setIsNewCalculate(true);
    };

    const handleCalculate = async () => {
        if (!n || !m || !p || matrix.length === 0) {
            setResult({ status: "Error", message: "Please enter n, m, p and matrix" });
            return;
        }

        try {
            const requestBody = {
                n: Number(n),
                m: Number(m),
                p: Number(p),
                matrix,
            };

            // call api calculate
            const response = await fetch("/calculate/do-calculate", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(requestBody),
            });

            if (!response.ok) throw new Error("Calculate error: " + response.data.message);

            const data = await response.json();
            setResult("Least Amount: " + data.data.output);

            if (isNewCalculate) {
                // call save history api if not load from history
                const saveHistoryResponse = await fetch("/calculate/save-history", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(requestBody),
                });

                if (!saveHistoryResponse.ok) throw new Error("Save history error: " + saveHistoryResponse.data.message);
                handleGetHistory()

            }

        } catch (err) {
            setResult("Error:" + err);
        }
    };

    const handleHistoryClick = (record, index) => {
        setN(record.n);
        setM(record.m);
        setP(record.p);
        setMatrix(record.matrix);
        setSelectedIndex(index);
        setResult(null)
        setIsNewCalculate(false);
    };

    const handleGetHistory = async () => {
        const response = await fetch("/calculate/get-history", {
            method: "GET",
            headers: { "Content-Type": "application/json" },
        });
        if (!response.ok) throw new Error(response.data.message);

        const data = await response.json();
        setHistory(data.data.output)
        setSelectedIndex(data.data.output.length)
    }

    return (
        <Box
            sx={{
                bgcolor: "#fafafa",
                minHeight: "100vh",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                p: 2,
            }}
        >
            <Container maxWidth="xl">
                <Grid container spacing={3}>
                    {/* Left side: Calculator */}
                    <Grid item xs={12} md={8}>
                        <Card sx={{ boxShadow: 3 }}>
                            <CardContent>
                                <Typography variant="h5" gutterBottom align="center">
                                    Treasure Calculator
                                </Typography>

                                <Grid container spacing={2}>
                                    <Grid item xs={4}>
                                        <TextField
                                            label="N (rows)"
                                            type="number"
                                            fullWidth
                                            value={n}
                                            onChange={(e) => { setN(e.target.value), setIsNewCalculate(true) } }
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <TextField
                                            label="M (cols)"
                                            type="number"
                                            fullWidth
                                            value={m}
                                            onChange={(e) => { setM(e.target.value), setIsNewCalculate(true) }}
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <TextField
                                            label="P (max chest)"
                                            type="number"
                                            fullWidth
                                            value={p}
                                            onChange={(e) => { setP(e.target.value), setIsNewCalculate(true) }}
                                        />
                                    </Grid>
                                </Grid>

                                {matrix.length > 0 && (
                                    <Box mt={3}>
                                        <Typography variant="subtitle1" align="center">
                                            Matrix:
                                        </Typography>
                                        <Table component={Paper}>
                                            <TableHead>
                                                <TableRow>
                                                    {Array.from({ length: m }).map((_, j) => (
                                                        <TableCell key={j} align="center">
                                                            Col {j + 1}
                                                        </TableCell>
                                                    ))}
                                                </TableRow>
                                            </TableHead>
                                            <TableBody>
                                                {matrix.map((row, i) => (
                                                    <TableRow key={i}>
                                                        {row.map((cell, j) => (
                                                            <TableCell key={j}>
                                                                <TextField
                                                                    type="number"
                                                                    value={cell}
                                                                    onChange={(e) =>
                                                                        handleMatrixChange(i, j, e.target.value)
                                                                    }
                                                                    size="small"
                                                                />
                                                            </TableCell>
                                                        ))}
                                                    </TableRow>
                                                ))}
                                            </TableBody>
                                        </Table>
                                    </Box>
                                )}

                                <Box mt={3} display="flex" justifyContent="center">
                                    <Button variant="contained" onClick={handleCalculate}>
                                        Calculate
                                    </Button>
                                </Box>

                                {result && (
                                    <Box mt={3}>
                                        <Typography variant="h6" align="center">
                                            Result:
                                        </Typography>
                                        <Paper sx={{ p: 2, bgcolor: "#f5f5f5" }}>
                                            <pre>{JSON.stringify(result, null, 2)}</pre>
                                        </Paper>
                                    </Box>
                                )}
                            </CardContent>
                        </Card>
                    </Grid>

                    {/* Right side: History */}
                    <Grid item xs={12} md={4}>
                        <Card sx={{ boxShadow: 3, height: "100%" }}>
                            <CardContent>
                                <Typography variant="h6" align="center" gutterBottom>
                                    History
                                </Typography>
                                <Table component={Paper}>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell>Time</TableCell>
                                            <TableCell>n</TableCell>
                                            <TableCell>m</TableCell>
                                            <TableCell>p</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {history.map((h, i) => (
                                            <TableRow
                                                key={i}
                                                hover
                                                sx={{
                                                    cursor: "pointer",
                                                    backgroundColor:
                                                        selectedIndex === i ? "#e3f2fd" : "inherit",
                                                }}
                                                onClick={() => handleHistoryClick(h, i)}
                                            >
                                                <TableCell>{new Date(h.createdAt).toLocaleString()}</TableCell>
                                                <TableCell>{h.n}</TableCell>
                                                <TableCell>{h.m}</TableCell>
                                                <TableCell>{h.p}</TableCell>
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </CardContent>
                        </Card>
                    </Grid>
                </Grid>
            </Container>
        </Box>
    );
}
