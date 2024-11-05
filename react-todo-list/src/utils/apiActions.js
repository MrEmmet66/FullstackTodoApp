export const fetchGraphQl = async (body) => {
    console.log(body)
    const response = await fetch('https://localhost:7255/graphql', {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "StorageType": localStorage.getItem("storageType") || 'MsSqlDb'
        },
        body: JSON.stringify(body),
    });
    const data = await response.json();
    return data;
}