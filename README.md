eneHibernateObservers
=====================

La idea es probar los observers de NHibernate en una DB real

* Cosas a tener en cuenta: 
  1. La db se crea sola cuando corres el test CreateDBConTutti()
  2. El injector de dependencias de test es el mismo que se usa para los controllers (el del framework WebApi2), así que pedirle la ISession a el

bla bla bla...
