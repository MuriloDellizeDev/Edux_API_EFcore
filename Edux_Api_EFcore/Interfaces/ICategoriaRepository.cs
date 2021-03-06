﻿using Edux_Api_EFcore.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edux_Api_EFcore.Interfaces
{
    interface ICategoriaRepository
    {

        List<Categoria> Listar();

        Categoria BuscarPorId(Guid id);

        List<Categoria> BuscarPorTipo(string tipo);

        Categoria Adicionar(Categoria categoria);

        void Editar(Categoria categoria);

        void Remover(Guid id);



    }
}
