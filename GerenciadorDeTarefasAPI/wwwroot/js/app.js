const apiUrl = "http://localhost:5141/api/tarefas";

// Função para obter e exibir tarefas (usada na página consultar.html)
async function getTarefas() {
    try {
        const response = await fetch(apiUrl);
        const tarefas = await response.json();
        const tarefasList = document.getElementById('tarefas-list');
        if (tarefasList) { // Verifica se a lista de tarefas está presente na página
            tarefasList.innerHTML = tarefas.map(tarefa =>
                `<li class="listatarefas" id="tarefa-${tarefa.id}">
                    ${tarefa.titulo}: ${tarefa.descricao}
                    <button class="excluir" onclick="excluirTarefa(${tarefa.id})">Excluir</button>
                </li>`
            ).join('');
        }
    } catch (error) {
        console.error('Erro ao obter tarefas:', error);
    }
}

// Função para excluir uma tarefa (usada na página consultar.html)
async function excluirTarefa(id) {
    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (response.ok) {
            // Remove a tarefa da interface do usuário
            document.getElementById(`tarefa-${id}`).remove();
        } else {
            console.error('Erro ao excluir a tarefa:', response.statusText);
        }
    } catch (error) {
        console.error('Erro ao excluir tarefa:', error);
    }
}

// Executa apenas na página de adicionar.html
if (document.getElementById('task-form')) {
    document.getElementById('task-form').addEventListener('submit', async (event) => {
        event.preventDefault();
        const titulo = document.getElementById('titulo').value;
        const descricao = document.getElementById('descricao').value;
        const tarefa = { titulo, descricao, concluida: false };

        try {
            const response = await fetch(apiUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(tarefa)
            });

            if (response.ok) {
                document.getElementById('task-form').reset();
                // Redireciona para a página de consulta para ver a tarefa adicionada
                window.location.href = "consultar.html";
            } else {
                console.error('Falha ao adicionar tarefa');
            }
        } catch (error) {
            console.error('Erro ao adicionar tarefa:', error);
        }
    });
}

// Executa apenas na página de consultar.html
if (document.getElementById('tarefas-list')) {
    document.addEventListener('DOMContentLoaded', getTarefas);
}
