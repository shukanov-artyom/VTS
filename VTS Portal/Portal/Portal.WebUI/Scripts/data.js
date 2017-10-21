
// initialize tree
$(function () {
    $('#data-tree').jstree();
    $('#data-tree').on("select_node.jstree", onNodeSelectedCallback);
});

// callback for node selection
function onNodeSelectedCallback(e, data) {
    var nodeDataType = data.node.data.datatype;
    var id = data.node.data.id;
    switch (nodeDataType) {
        case 'psa-dataset':
            displayDataset(id);
            break;
        case 'psa-trace':
            displayTrace(id);
            break;
        case 'psa-parameters-set':
            displayParametersSet(id);
            break;
        case 'psa-parameter':
            displayParameterData(id);
            break;
    }
}

function displayDataset(datasetId) {
    document.getElementById('chart-view-div').innerHTML = null;
}

function displayTrace(traceId) {
    document.getElementById('chart-view-div').innerHTML = null;
}

function displayParametersSet(setId) {
    document.getElementById('chart-view-div').innerHTML = null;
}

function displayParameterData(parameterDataId) {
    $('#chart-view-div').load('./ParameterChart?parameterDataId=' + parameterDataId);
}